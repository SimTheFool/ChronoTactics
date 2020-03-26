using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class GridPrototypeWizard : EditorWindow
{
    // Data parameters
    private GridPrototype gridProto;
    private CellPrototype[] cellPrototypes;

    // Control parameters
    private string[] cellTypes;
    private Dictionary<Vector2Int, int> cellIndexPerControl = new Dictionary<Vector2Int, int>();
    private bool isNew;
    private string copyName;

    // Layout parameters
    private Vector2 scroll = new Vector2();
    private int cellPickerDim = 16;

    [MenuItem("Tools/GridPrototypeWizard")]
    public static void Open()
    {
        GridPrototypeWizard window = (GridPrototypeWizard)GetWindow(typeof(GridPrototypeWizard));
        window.Show();
    }

    [OnOpenAssetAttribute(1)]
    public static bool OnOpenGridPrototypeAsset(int instanceId, int line)
    {
         if(EditorUtility.InstanceIDToObject(instanceId) is GridPrototype)
         {
             GridPrototypeWizard.Open();
         }
         return false;
    }

    public void Awake()
    {
        // Control initialization
        if(Selection.activeObject is GridPrototype)
        {
            this.gridProto = (GridPrototype)Selection.activeObject;
            this.isNew= false;
        }
        else
        {
            this.gridProto = ScriptableObject.CreateInstance<GridPrototype>();
            this.isNew = true;
        }

        this.cellPrototypes = Resources.LoadAll<CellPrototype>("Cell Prototypes");
        this.cellTypes = this.cellPrototypes.Select(elm => {
            return elm.ToString();
        }).ToArray();

        foreach(KeyValuePair<Vector2Int, CellPrototype> kvp in this.gridProto.grid)
        {
            int id = Array.IndexOf(this.cellPrototypes, kvp.Value);
            id = Mathf.Max(0, id);
            this.cellIndexPerControl[kvp.Key] = id;
        }
    }

    public void OnGUI()
    {
        this.scroll = GUILayout.BeginScrollView(this.scroll);

        this.DrawGridNameControl();
        this.DrawWidthAndHeightControl();
        GUILayout.Space(50);

        for(int y = this.gridProto.height; y > 0 ; y--)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(50);
            for(int x = 0; x < this.gridProto.width; x++)
            {
                this.DrawCellPicker(x, y);
            }
            GUILayout.Space(50);
            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();

        this.DrawSaveButton();
    }

    public void DrawGridNameControl()
    {
        this.gridProto.gridName = EditorGUILayout.TextField("Grid name", this.gridProto.gridName);
    }

    public void DrawWidthAndHeightControl()
    {
        this.gridProto.width = EditorGUILayout.IntField("width", this.gridProto.width);
        this.gridProto.height = EditorGUILayout.IntField("height", this.gridProto.height);
    }

    public void DrawCellPicker(int x, int y)
    {
        GUILayout.BeginVertical(new GUILayoutOption[]{GUILayout.Width(this.cellPickerDim), GUILayout.Height(this.cellPickerDim)});

        int index;
        if(!this.cellIndexPerControl.TryGetValue(new Vector2Int(x, y), out index))
        {
            index = 0;
        }

        index = EditorGUILayout.Popup(index, this.cellTypes, new GUILayoutOption[]{GUILayout.Width(this.cellPickerDim), GUILayout.Height(this.cellPickerDim)});

        this.cellIndexPerControl[new Vector2Int(x ,y)] = index;
        this.gridProto.grid[new Vector2Int(x ,y)] = this.cellPrototypes[index];

        GUILayout.EndVertical();
        this.DrawSpriteViewer(index);
    }

    public void DrawSpriteViewer(int index) {
        Rect group = GUILayoutUtility.GetLastRect();
        CellPrototype proto = this.cellPrototypes[index];
        Rect spriteRect = new Rect(proto.sprite.rect.x / proto.sprite.texture.width, proto.sprite.rect.y / proto.sprite.texture.height,
                     proto.sprite.rect.width / proto.sprite.texture.width, proto.sprite.rect.height / proto.sprite.texture.height);
        GUI.DrawTextureWithTexCoords(group, proto.sprite.texture, spriteRect);
    }

    public void DrawSaveButton()
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if(GUILayout.Button((this.isNew) ? "Save" : "Copy As", new GUILayoutOption[]{GUILayout.Width(64)}))
        {
            GridPrototype proto = Instantiate(this.gridProto);
            AssetDatabase.CreateAsset (proto, $"Assets/Resources/Grid Prototypes/{((this.isNew) ? this.gridProto.gridName : this.copyName)}.asset");
        }

        if(!this.isNew)
        {
            this.copyName = EditorGUILayout.TextField(this.copyName);
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

}
