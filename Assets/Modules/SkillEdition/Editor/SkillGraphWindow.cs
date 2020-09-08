using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class SkillGraphWindow : EditorWindow
{
    private SkillGraph rootGraphView = null;
    private Toolbar toolbar = null;

    [MenuItem("Graph/Skill Graph")]
    public static void OpenSkillGraphWindow()
    {
        SkillGraphWindow window = GetWindow<SkillGraphWindow>();
        window.titleContent = new UnityEngine.GUIContent("Skill Graph");
    }

    void OnEnable()
    {
        this.rootGraphView = new SkillGraph();
        this.rootVisualElement.Add(this.rootGraphView);

        this.toolbar = new Toolbar();
        this.rootVisualElement.Add(this.toolbar);

        TextField nameTextField = new TextField("");
        this.toolbar.Add(nameTextField);

        Button saveButton = new Button();
        saveButton.text = "Save";
        saveButton.clicked += () => this.Save();
        this.toolbar.Add(saveButton);
    }

    void OnDisable()
    {
        this.rootVisualElement.Remove(this.rootGraphView);
        this.rootVisualElement.Remove(this.toolbar);
    }

    private void Save()
    {

    }
}