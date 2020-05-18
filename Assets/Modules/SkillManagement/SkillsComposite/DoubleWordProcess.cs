using System.Collections.Generic;
using System.Linq;

public class DoubleWordProcess :  SkillProcess
{
    private string word = "";

    private string outputWord = "";
    public string OutputWord => this.outputWord;

    public DoubleWordProcess(string word = "")
    {
        this.word = word;
    }

    public override bool Process()
    {
        this.word += this.word;
        this.outputWord = this.word;
        return true;
    }
}