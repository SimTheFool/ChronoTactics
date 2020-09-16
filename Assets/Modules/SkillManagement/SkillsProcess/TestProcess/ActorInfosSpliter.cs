using UnityEngine;

[SkillGraphNode(SkillGraphNodeAttribute.NodeTag.Other)]
public class ActorInfosSpliter : SkillProcess
{
    [SkillGraphPort(SkillGraphPortAttribute.Direction.Input)]
    public ActorFacade actor = null;

    [SkillGraphPort(SkillGraphPortAttribute.Direction.Output)]
    public string name = "";

    public override bool Process()
    {
        this.name = actor.Name;
        return true;
    }
}