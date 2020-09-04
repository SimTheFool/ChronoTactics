using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class SkillGraphNode : Node
{
    protected void AddOutputPort(string portName, Type type)
    {
        Port port = this.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, type);
        port.portName = portName;
        port.portColor = Color.black;
        this.outputContainer.Add(port);

        this.Refresh();
    }

    protected void AddInputPort(string portName, Type type)
    {
        Port port = this.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, type);
        port.portName = portName;
        port.portColor = Color.black;
        this.inputContainer.Add(port);

        this.Refresh();
    }

    private void Refresh()
    {
        this.RefreshExpandedState();
        this.RefreshPorts();
    }
}