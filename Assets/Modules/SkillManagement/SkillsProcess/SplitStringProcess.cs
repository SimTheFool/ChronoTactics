using UnityEngine;

public class SplitStringProcess : SkillProcess
{
    private string str = "";
    private char separator = '.';

    public string[] words;

    public SplitStringProcess(string str, char separator)
    {
        this.str = str;
    }

    public override bool Process()
    {
        this.words = str.Split(this.separator);
        return true;
    }
}