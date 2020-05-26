using System.Collections.Generic;
using UnityEngine;

public class MoveProcess : SkillProcess
{

    private float moveSpeed = 2.0f;
    private float treshold = 0.05f;

    private MoveAlongPathProcess moveAlongPathProcess = null;

    public MoveProcess(ActorFacade actor, TileFacade dest, ITopology topology, IFilter filter)
    {
        HashSet<TileFacade> path = DependencyLocator.getTileFinder().findPath(actor.GetTile(), dest, topology, filter);
        this.moveAlongPathProcess = new MoveAlongPathProcess(actor, path, null, this.moveSpeed, this.treshold);
    }

    public override bool Process()
    {
        return this.moveAlongPathProcess.Process();
    }
}