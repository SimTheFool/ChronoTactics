using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SkillProcessDatas : ISerializationCallbackReceiver
{
    public string Id;
    public string ProcessType;
    public Vector4 Position;
    public List<(string inputName, string connectedOutputName, string connectedNodeId)> InputsDatas = new List<(string inputName, string connectedOutputName, string connectedNodeId)>();

    // ISerializationCallbackReceiver implementation.

    [SerializeField]
    private List<string> inputNames = new List<string>();

    [SerializeField]
    private List<string> connectedOutputNames = new List<string>();

    [SerializeField]
    private List<string> connectedNodeIds = new List<string>();

    public void OnBeforeSerialize()
    {
        foreach ((string inputName, string connectedOutputName, string connectedNodeId) in this.InputsDatas)
        {
            this.inputNames.Clear();
            this.inputNames.Add(inputName);

            this.connectedOutputNames.Clear();
            this.connectedOutputNames.Add(connectedOutputName);

            this.connectedNodeIds.Clear();
            this.connectedNodeIds.Add(connectedNodeId);
        }
    }

    public void OnAfterDeserialize()
    {
        for(int i = 0; i < this.inputNames.Count; i++)
        {
            this.InputsDatas.Clear();
            this.InputsDatas.Add((this.inputNames[i], this.connectedOutputNames[i], this.connectedNodeIds[i]));
        }
    }
}