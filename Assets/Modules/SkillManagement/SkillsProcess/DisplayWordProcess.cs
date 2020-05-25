using UnityEngine;

public class DisplayWordProcess : SkillProcess
{
    private string wordToDisplay = null;

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