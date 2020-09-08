using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SkillProcessDatas
{
    public string Id {get; set;}
    public string ProcessType {get; set;}
    public Vector2 Position {get; set;}
    public IEnumerable<(string inputName, string connectedOutputName, string connectedNodeId)> InputsDatas {get; set;}
}