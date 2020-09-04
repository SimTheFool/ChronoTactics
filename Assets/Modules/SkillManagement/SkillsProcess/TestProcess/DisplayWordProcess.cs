using UnityEngine;

[SkillGraphNode]
public class DisplayWordProcess : SkillProcess
{
    [SkillGraphPort(SkillGraphPortAttribute.Direction.Input)]
    public string wordToDisplay = null;

    public DisplayWordProcess(string word)
    {
        this.wordToDisplay = word;
    }

    public override bool Process()
    {
        Debug.Log(this.wordToDisplay);
        return true;
    }
}