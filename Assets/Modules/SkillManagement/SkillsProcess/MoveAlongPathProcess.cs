using System.Collections.Generic;
using UnityEngine;

public class MoveAlongPathProcess : SkillProcess
{
    private float moveSpeed;
    private float treshold;

    private Transform actorTransform = null;
    private ActorFacade actor = null;
    
    private Queue<TileFacade> path;
    private IFilter filter = null;

    private TileFacade nextTile = null;

    public MoveAlongPathProcess(ActorFacade actor, IEnumerable<TileFacade> path, IFilter filter = null, float moveSpeed = 2.0f, float treshold = 0.05f)
    {
        this.moveSpeed = moveSpeed;
        this.treshold= treshold;

        this.actor = actor;
        this.actorTransform = actor.transform;

        this.path = new Queue<TileFacade>(path);
        this.filter = filter;
    }

    public override bool Process()
    {
        if(this.nextTile == null)
            return !this.MoveToNextTile();

        this.actorTransform.position =  Vector3.MoveTowards(this.actorTransform.position, nextTile.WorldPos, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(this.actorTransform.position, nextTile.WorldPos) < this.treshold)
        {
            this.actor.SetTile(nextTile);
            return !this.MoveToNextTile();
        }
        else
        {
            return false;
        }
    }

    public bool MoveToNextTile()
    {
        if(this.path.Count == 0)
        {
            this.nextTile = null;
            return false;
        }

        TileFacade tile = this.path.Dequeue();

        if(tile == null || (this.filter != null && !this.filter.IsAccessible(tile)))
        {
            this.nextTile = null;
            return false;
        }

        this.nextTile = tile;
        return true;
    }
}