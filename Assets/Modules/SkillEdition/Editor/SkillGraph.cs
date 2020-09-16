using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;
using System.Collections.Generic;

public class SkillGraph :  GraphView
{
    private SkillInputNode entrypoint = null;

    public SkillGraph()
    {
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(new ContentZoomer());

        this.StretchToParentSize();

        this.entrypoint = new SkillInputNode();
        this.AddElement(this.entrypoint);
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        Dictionary<SkillGraphNodeAttribute.NodeTag, IEnumerable<Type>> tagsToProcesses = Assembly.GetAssembly(typeof(SkillProcess)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(SkillProcess)) && myType.GetCustomAttribute<SkillGraphNodeAttribute>() != null)
            .GroupBy(myType => myType.GetCustomAttribute<SkillGraphNodeAttribute>().nodeTag, myType => myType)
            .ToDictionary(myGroup => myGroup.Key, myGroup => myGroup.AsEnumerable());

        foreach(SkillGraphNodeAttribute.NodeTag tag in tagsToProcesses.Keys)
        {
            foreach(Type type in tagsToProcesses[tag])
            {
                evt.menu.AppendAction($"{tag.ToString()}/{type.ToString()}", (dropdownMenuAction) => this.CreateSkillProcess(type : type, position : dropdownMenuAction.eventInfo.localMousePosition), DropdownMenuAction.Status.Normal);
            }
        }
    }

    private SkillProcessNode CreateSkillProcess(Type type, Guid id = default(Guid), Vector2 position = new Vector2())
    {
        SkillProcessNode node = new SkillProcessNode(type, id);
        Rect rect = node.GetPosition();
        rect.x = position.x;
        rect.y = position.y;
        node.SetPosition(rect);
        this.AddElement(node);

        return node;
    }

    private SkillInputNode CreateEntryPoint(Guid id = default(Guid))
    {
        SkillInputNode newEntrypoint = new SkillInputNode(id);
        this.RemoveElement(this.entrypoint);
        this.entrypoint = newEntrypoint;
        this.AddElement(this.entrypoint);

        return newEntrypoint;
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compatiblePorts = new List<Port>();

        this.ports.ForEach(port => {                
                if(startPort != port && startPort.node != port.node && startPort.portType == port.portType)
                    compatiblePorts.Add(port);
        });

        return compatiblePorts;
    }

    public List<SkillNodeDatas> GetSkillDatasFromNodes()
    {
        List<SkillNodeDatas> skillDatas = new List<SkillNodeDatas>();

        this.nodes.ForEach(n => {
            SkillGraphNode node = n as SkillGraphNode;
            if(node != null)
                skillDatas.Add(node.GetDatasFromNode());
        });

        return skillDatas;
    }

    public void SetNodesFromSkillDatas(IEnumerable<SkillNodeDatas> skillDatas)
    {
        if(skillDatas == null)
            return;

        Dictionary<Guid, SkillGraphNode>  nodeRegistry = new Dictionary<Guid, SkillGraphNode>();
        List<(SkillGraphNode node, SkillNodeDatas datas)> nodesToDatas = new List<(SkillGraphNode node, SkillNodeDatas datas)>();

        foreach(SkillNodeDatas datas in skillDatas)
        {
            Guid id = Guid.Parse(datas.Id);
            SkillGraphNode node;

            switch(datas.NodeType)
            {
                case NodeType.EntryPoint:
                    node = this.CreateEntryPoint();
                    break;

                case NodeType.Process:
                    Type type = Assembly.GetAssembly(typeof(SkillProcess)).GetType(datas.ProcessType);
                    if(type == null)
                        continue;
                    node = this.CreateSkillProcess(type: type, id: id);
                    break;

                default:
                    node = null;
                    break;
            }

            if(node == null)
                continue;

            this.AddElement(node);

            nodeRegistry.Add(id, node);
            nodesToDatas.Add((node, datas));
        }

        foreach((SkillGraphNode node, SkillNodeDatas datas) in nodesToDatas)
        {
            IEnumerable<Edge> edges = node.SetNodeFromDatas(datas, nodeRegistry);

            foreach (Edge edge in edges)
            {
                if(this.GetCompatiblePorts(edge.input, null).Any(port => port == edge.output))
                {
                    this.AddElement(edge);
                }
                else
                {
                    edge.input.Disconnect(edge);
                    edge.output.Disconnect(edge);
                }
            }
        }
    }
}