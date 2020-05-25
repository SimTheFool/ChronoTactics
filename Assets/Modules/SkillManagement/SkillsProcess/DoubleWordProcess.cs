using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoubleWordProcess :  SkillProcess
{
    private string word = "";

    private string outputWord = "";
    public string OutputWord => this.outputWord;

    private int count = 0;

    public DoubleWordProcess(string word = "")
    {
        this.word = word;
    }

    public override bool Process()
    {
        this.outputWord = this.word + this.word;
        this.count++;
        return true;
    }
}