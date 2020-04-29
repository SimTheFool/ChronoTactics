using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Skill))]
public class SkillEditor : Editor
{
    private SerializedProperty skillName;
    private SerializedProperty commandsName;

    private int count;
    private string[] allCommandsName;

    void OnEnable()
    {
        this.skillName = serializedObject.FindProperty("skillName");
        this.commandsName = serializedObject.FindProperty("commandsName");

        this.count = this.commandsName.Copy().arraySize;

        this.allCommandsName = new string[SkillCommandRegistry.skillCommands.Count];
        SkillCommandRegistry.skillCommands.Keys.CopyTo(allCommandsName, 0);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(this.skillName);
        this.count = EditorGUILayout.DelayedIntField("Skill Length", this.count);

        for (int i = 0; i < Mathf.Max(this.count, this.commandsName.arraySize); i++)
        {
            if(i >= this.count)
            {
                this.commandsName.DeleteArrayElementAtIndex(i);
                continue;
            }

            if(i >= this.commandsName.arraySize)
            {
                this.commandsName.InsertArrayElementAtIndex(i);
                this.commandsName.GetArrayElementAtIndex(i).stringValue = "";
            }

            int index = System.Array.FindIndex(allCommandsName, name => name == this.commandsName.GetArrayElementAtIndex(i).stringValue);
            index = UnityEditor.EditorGUILayout.Popup(index, allCommandsName);
            this.commandsName.GetArrayElementAtIndex(i).stringValue = allCommandsName[index];
        }

        serializedObject.ApplyModifiedProperties();
    }
}