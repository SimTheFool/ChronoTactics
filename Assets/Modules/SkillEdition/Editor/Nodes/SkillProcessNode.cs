using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;

public class SkillProcessNode : SkillGraphNode
{
    public SkillProcessNode(Type compositeType) : base()
    {
        this.processType  = compositeType;
        this.nodeType = SkillGraphNodeType.Composite;

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

    public override SkillProcessDatas GetSkillProcessDatas()
    {
        SkillProcessDatas processDatas = new SkillProcessDatas();

        processDatas.Id = this.id.ToString();
        processDatas.ProcessType = this.processType.ToString();
        processDatas.Position = new Vector2(this.GetPosition().x, this.GetPosition().y);

        List<Port> inputPorts = this.ports.Where(port => port.direction == Direction.Input).ToList();

        List<(string inputName, string connectedOutputName, string connectedNodeId)> inputsDatas = new List<(string inputName, string connectedOutputName, string connectedNodeId)>();

        foreach(Port input in inputPorts)
        {
            (string inputName, string connectedOutputName, string connectedNodeId) inputDatas;
            Port output = input.connections.First().output;

            inputDatas.inputName = input.portName;
            inputDatas.connectedOutputName = output.portName;
            inputDatas.connectedNodeId = ((SkillGraphNode)output.node).Id.ToString();

            inputsDatas.Add(inputDatas);
        }

        processDatas.InputsDatas = inputsDatas.AsEnumerable();

        return processDatas;
    }
}