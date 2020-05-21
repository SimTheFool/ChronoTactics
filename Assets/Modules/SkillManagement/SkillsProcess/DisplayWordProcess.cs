using UnityEngine;

public class DisplayWordProcess : SkillProcess
{
    private string wordToDisplay = null;
    private int count = 0;

    public DisplayWordProcess(string word)
    {
        this.wordToDisplay = word;
    }

    public override bool Process()
    {
        this.count++;
        Debug.Log(this.wordToDisplay+ " " + this.count);
        return true;
    }
    
    public override void DebugStr()
    {
        Debug.Log("a");
        Debug.Log(wordToDisplay);
    }
}