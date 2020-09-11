using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public enum SkillGraphNodeType {Composite, Input};

public abstract class SkillGraphNode : Node
{
    protected Guid id;
    public Guid Id => this.id;

    protected Type processType = null;
    
    protected List<Port> ports = new List<Port>();
    public IEnumerable<Port> Ports => this.ports.AsEnumerable();

    protected void AddOutputPort(string portName, Type type)
    {
        Port port = this.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, type);
        port.portName = portName;
        port.portColor = Color.black;
        this.outputContainer.Add(port);
        this.ports.Add(port);

        this.Refresh();
    }

    protected void AddInputPort(string portName, Type type)
    {
        Port port = this.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, type);
        port.portName = portName;
        port.portColor = Color.black;
        this.inputContainer.Add(port);
        this.ports.Add(port);

        this.Refresh();
    }

    private void Refresh()
    {
        this.RefreshExpandedState();
        this.RefreshPorts();
    }

    public abstract SkillProcessDatas GetSkillProcessDatasFromNode();

    public abstract IEnumerable<Edge> SetNodeFromSkillProcessDatas(SkillProcessDatas datas, Dictionary<Guid, SkillGraphNode> nodeRegistry);
}