using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillGraphWindow : EditorWindow
{
    private SkillGraph rootGraphView = null;

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
    }

    void OnDisable()
    {
        this.rootVisualElement.Remove(this.rootGraphView);
    }
}