using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System;
using System.Collections.Generic;

public class SkillInputNode : SkillGraphNode
{
    public SkillInputNode(Guid id = default(Guid)) : base(id)
    {        
        this.nodeType = NodeType.EntryPoint;
        this.processType  = null;

        this.title = "ENTRYPOINT";

        this.AddOutputPort("Caster", typeof(ActorFacade));
        this.AddOutputPort("Target tile", typeof(TileFacade));
        this.AddOutputPort("Target actor", typeof(ActorFacade));
    }

    public override bool IsMovable()
    {
        return false;
    }

    public override bool IsAscendable()
    {
        return false;
    }

    public override SkillNodeDatas GetDatasFromNode()
    {
        SkillNodeDatas entrypointDatas = new SkillNodeDatas();

        entrypointDatas.Id = this.id.ToString();
        entrypointDatas.ProcessType = null;
        entrypointDatas.NodeType = NodeType.EntryPoint;

        return entrypointDatas;
    }

    public override IEnumerable<Edge> SetNodeFromDatas(SkillNodeDatas datas, Dictionary<Guid, SkillGraphNode> nodeRegistry)
    {
        return new List<Edge>();
    }
}