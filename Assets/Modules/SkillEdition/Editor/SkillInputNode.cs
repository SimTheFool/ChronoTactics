using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System;

public class SkillInputNode : SkillGraphNode
{
    public SkillInputNode()
    {
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
}