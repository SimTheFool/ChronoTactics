using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;
using System.Collections.Generic;

public class SkillGraph :  GraphView
{
    public SkillGraph()
    {
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(new ContentZoomer());

        this.StretchToParentSize();
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        Dictionary<SkillGraphNodeAttribute.NodeTag, IEnumerable<Type>> tagsToProcesses = Assembly.GetAssembly(typeof(SkillProcess)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(SkillProcess)) && myType.GetCustomAttribute<SkillGraphNodeAttribute>() != null)
            .GroupBy(myType => myType.GetCustomAttribute<SkillGraphNodeAttribute>().nodeTag, myType => myType)
            .ToDictionary(myGroup => myGroup.Key, myGroup => myGroup.AsEnumerable());

        foreach(SkillGraphNodeAttribute.NodeTag tag in tagsToProcesses.Keys)
        {
            evt.menu.AppendSeparator();
            evt.menu.AppendAction(tag.ToString(), (dropdownMenuAction) => Debug.Log("Label"), DropdownMenuAction.Status.Disabled);
            evt.menu.AppendSeparator();

            foreach(Type type in tagsToProcesses[tag])
            {
                evt.menu.AppendAction(type.ToString(), (dropdownMenuAction) => this.AddElement(new SkillProcessNode(type)), DropdownMenuAction.Status.Normal);
            }
        }
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

    public List<SkillProcessDatas> GetSkillDatasFromNodes()
    {
        List<SkillProcessDatas> skillDatas = new List<SkillProcessDatas>();

        this.nodes.ForEach(n => {
            SkillGraphNode node = n as SkillGraphNode;
            if(node != null)
                skillDatas.Add(node.GetSkillProcessDatasFromNode());
        });

        return skillDatas;
    }

    public void SetNodesFromSkillDatas(IEnumerable<SkillProcessDatas> skillDatas)
    {
        if(skillDatas == null)
            return;

        Dictionary<Guid, SkillGraphNode>  nodeRegistry = new Dictionary<Guid, SkillGraphNode>();
        List<(SkillProcessNode node, SkillProcessDatas datas)> nodesToDatas = new List<(SkillProcessNode node, SkillProcessDatas datas)>();

        foreach(SkillProcessDatas datas in skillDatas)
        {
            Type type = Assembly.GetAssembly(typeof(SkillProcess)).GetType(datas.ProcessType);

            if(type == null)
                continue;

            Guid id = Guid.Parse(datas.Id);

            SkillProcessNode node = new SkillProcessNode(type, id);
            this.AddElement(node);

            nodeRegistry.Add(id, node);
            nodesToDatas.Add((node, datas));
        }

        foreach((SkillProcessNode node, SkillProcessDatas datas) in nodesToDatas)
        {
            IEnumerable<Edge> edges = node.SetNodeFromSkillProcessDatas(datas, nodeRegistry);

            foreach (Edge edge in edges)
            {
                this.AddElement(edge);
            }
        }
    }
}