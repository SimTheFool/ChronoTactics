using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;

public class SkillProcessNode : SkillGraphNode
{
    public SkillProcessNode(Type processType, Guid id = default(Guid)) : base(id)
    {
        this.nodeType = NodeType.Process;
        this.processType  = processType;

        this.title = this.processType.ToString();

        Dictionary<SkillGraphPortAttribute.Direction, IEnumerable<(string name, Type type, SkillGraphPortAttribute portAttribute)>> portsInfosPerDirections;

        portsInfosPerDirections = this.processType.GetFields()
            .Select(fieldInfo => {
                (string name, Type type, SkillGraphPortAttribute portAttribute) result;

                result.name = fieldInfo.Name;
                result.type = fieldInfo.FieldType;
                result.portAttribute = fieldInfo.GetCustomAttribute<SkillGraphPortAttribute>();

                return result;
            })
            .Where(result => result.portAttribute != null)
            .GroupBy(result => result.portAttribute.direction)
            .ToDictionary(result => result.Key, result => result.AsEnumerable());

        foreach(SkillGraphPortAttribute.Direction direction in portsInfosPerDirections.Keys)
        {
            Action<string, Type> addPort = (direction == SkillGraphPortAttribute.Direction.Input) ? (Action<string, Type>)this.AddInputPort : this.AddOutputPort;

            foreach((string name, Type type, SkillGraphPortAttribute portAttribute) infos in portsInfosPerDirections[direction])
            {
                addPort(infos.name, infos.type);
            }
        }
    }

    public override SkillNodeDatas GetDatasFromNode()
    {
        SkillNodeDatas processDatas = new SkillNodeDatas();

        processDatas.Id = this.id.ToString();
        processDatas.ProcessType = this.processType.ToString();
        processDatas.NodeType = NodeType.Process;
        Rect rect = this.GetPosition();
        processDatas.Position = new Vector4(rect.x, rect.y, rect.width, rect.height);

        List<Port> inputPorts = this.ports.Where(port => port.direction == Direction.Input).ToList();

        List<(string inputName, string connectedOutputName, string connectedNodeId)> inputsDatas = new List<(string inputName, string connectedOutputName, string connectedNodeId)>();

        foreach(Port input in inputPorts)
        {
            (string inputName, string connectedOutputName, string connectedNodeId) inputDatas;
            Port output = input.connections.FirstOrDefault()?.output;

            inputDatas.inputName = input.portName;
            inputDatas.connectedOutputName = output?.portName;
            inputDatas.connectedNodeId = (output != null) ? ((SkillGraphNode)output?.node).Id.ToString() : null;

            inputsDatas.Add(inputDatas);
        }

        processDatas.InputsDatas = inputsDatas;

        return processDatas;
    }

    public override IEnumerable<Edge> SetNodeFromDatas(SkillNodeDatas datas, Dictionary<Guid, SkillGraphNode> nodeRegistry)
    {
        List<Edge> edges = new List<Edge>();

        this.SetPosition(new Rect(datas.Position.x, datas.Position.y, datas.Position.y, datas.Position.z));

        foreach((string inputName, string connectedOutputName, string connectedNodeId) in datas.InputsDatas)
        {
            Port inputPort = this.ports.FirstOrDefault(port => port.portName == inputName);

            Guid tempGuid;
            if(!Guid.TryParse(connectedNodeId, out tempGuid))
                continue;

            Port outputPort = nodeRegistry[tempGuid]?.Ports.FirstOrDefault(port => port.portName == connectedOutputName);

            if(outputPort == null)
                continue;

            edges.Add(inputPort.ConnectTo(outputPort));
        }

        return edges;
    }
}