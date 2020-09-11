using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;
using System.Linq;

public class SkillGraphWindow : EditorWindow
{
    private SkillGraph graph = null;
    private Toolbar toolbar = null;
    private TextField nameTextField = null;


    private Object skillAsset = null;
    private Skill skill = null;

    [OnOpenAssetAttribute(1)]
    public static bool OpenSkillGraphWindow(int instanceId, int line)
    {
        if(!(Selection.activeObject is Skill))
            return false;

        SkillGraphWindow window = CreateInstance<SkillGraphWindow>();
        window.titleContent = new UnityEngine.GUIContent("Skill Graph");
        window.LoadSkill(Selection.activeObject);
        window.Show();

        return true;
    }

    void OnEnable()
    {
        this.graph = new SkillGraph();
        this.rootVisualElement.Add(this.graph);

        this.toolbar = new Toolbar();
        this.rootVisualElement.Add(this.toolbar);

        this.nameTextField = new TextField();
        this.nameTextField.style.width = 200;
        this.toolbar.Add(this.nameTextField);

        Button saveButton = new Button();
        saveButton.text = "Save";
        saveButton.clicked += () => this.SaveSkill();
        this.toolbar.Add(saveButton);

        if(this.skillAsset != null)
            this.LoadSkill(this.skillAsset);
    }

    void OnDisable()
    {
        this.rootVisualElement.Remove(this.graph);
        this.rootVisualElement.Remove(this.toolbar);
    }

    private void SaveSkill()
    {
        if(this.skillAsset == null)
            return;

        this.skill.SkillDatas = this.graph.GetSkillDatasFromNodes();
        this.skill.Label = this.nameTextField.value;

        string skillAssetPath = AssetDatabase.GetAssetPath(this.skillAsset);
        AssetDatabase.RenameAsset(skillAssetPath, this.nameTextField.value);

        EditorUtility.SetDirty(this.skillAsset);
        EditorUtility.SetDirty(this.skill);
    }

    private void LoadSkill(Object skillAsset)
    {
        this.skillAsset = skillAsset;
        this.skill = skillAsset as Skill;
        this.nameTextField.value = this.skillAsset.name;

        this.graph.SetNodesFromSkillDatas(this.skill.SkillDatas);
    }
}