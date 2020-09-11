using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System;
using System.Collections.Generic;

public class SkillInputNode : SkillGraphNode
{
    public SkillInputNode() : base()
    {
        this.id = new Guid();
        
        this.title = "Input";

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

    public override SkillProcessDatas GetSkillProcessDatasFromNode()
    {
        return null;
    }

    public override IEnumerable<Edge> SetNodeFromSkillProcessDatas(SkillProcessDatas datas, Dictionary<Guid, SkillGraphNode> nodeRegistry)
    {
        return null;
    }
}