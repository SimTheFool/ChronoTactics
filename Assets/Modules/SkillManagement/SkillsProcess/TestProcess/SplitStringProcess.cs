using UnityEngine;
using System.Linq;

[SkillGraphNode]
public class SplitStringProcess : SkillProcess
{
    [SkillGraphPort(SkillGraphPortAttribute.Direction.Input)]
    public string str = "";

    [SkillGraphPort(SkillGraphPortAttribute.Direction.Input)]
    public char separator = '.';

    [SkillGraphPort(SkillGraphPortAttribute.Direction.Output)]
    public string[] words;

    public SplitStringProcess(string str, char separator)
    {
        this.str = str;
        this.separator = separator;
    }

    public override bool Process()
    {
        this.words = str.Split(this.separator);
        return true;
    }

    public override string ToString()
    {
        return this.GetType().ToString() +"Input: " + this.str + " " + this.words.Aggregate("Output: ", (acc, elem) => acc + " - " + elem);
    }
}