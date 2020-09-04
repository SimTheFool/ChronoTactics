using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SkillGraphNode]
public class DoubleWordProcess :  SkillProcess
{
    [SkillGraphPort(SkillGraphPortAttribute.Direction.Input)]
    public string word = "";

    [SkillGraphPort(SkillGraphPortAttribute.Direction.Output)]
    public string outputWord = "";

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