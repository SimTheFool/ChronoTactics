using UnityEngine;
using System.Linq;

public class SplitStringProcess : SkillProcess
{
    private string str = "";
    private char separator = '.';

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