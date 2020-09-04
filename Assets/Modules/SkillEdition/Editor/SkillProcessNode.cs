using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;

public class SkillProcessNode : SkillGraphNode
{
    public SkillProcessNode(Type type)
    {
        this.title = type.ToString();

        Dictionary<SkillGraphPortAttribute.Direction, IEnumerable<(string name, Type type, SkillGraphPortAttribute portAttribute)>> portsInfosPerDirections;

        portsInfosPerDirections = type.GetFields()
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
}