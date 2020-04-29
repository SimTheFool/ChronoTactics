using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/Skill", order = 1)]
public class Skill : ScriptableObject
{

    [SerializeField]
    private string skillName = "";
    public string Name
    {
        get
        {
            return this.skillName;
        }
    }

    [SerializeField]
    private List<string> commandsName = new List<string>();
    private List<SkillCommand> commandsList = null;

    private Queue<SkillCommand> commands;
    private SkillCommand currentCommand = null;

    void OnEnable()
    {
        this.commandsList = new List<SkillCommand>();

        foreach(string name in this.commandsName)
        {
            this.commandsList.Add(SkillCommandRegistry.skillCommands[name]);
        }
    }

    public bool Process(SkillInput input)
    {
        bool done;

        if(this.currentCommand != null)
        {
            done = this.currentCommand.Process(input);
        }
        else
        {
            done = true;
        }

        if(done == true)
        {
            bool result = this.MoveToNextCommand(input);
            return !result;
        }

        return false;
    }

    public void Init(SkillInput input)
    {
        this.commands = new Queue<SkillCommand>(this.commandsList);
        this.MoveToNextCommand(input);
    }

    public bool MoveToNextCommand(SkillInput input)
    {
        if(this.commands.Count == 0)
        {
            this.currentCommand = null;
            return false;
        }

        this.currentCommand = this.commands.Dequeue();
        this.currentCommand.Init(input);
        return true;
    }
}