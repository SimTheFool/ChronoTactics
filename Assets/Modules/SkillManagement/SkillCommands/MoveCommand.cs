using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : SkillCommand
{
    private float moveSpeed = 2.0f;
    private float treshold = 0.05f;

    private Queue<TileFacade> tilesPath;
    private ITopology topology = new CrowFlyTopology();
    private IFilter filter = new WalkableFilter();

    private TileFacade nextTile = null;

    public override void Init(SkillInput input)
    {
        HashSet<TileFacade> tilesPath = DependencyLocator.getPathfinder().findPath(input.Caster.Tile, input.TargetTile, this.topology, this.filter);
        this.tilesPath = new Queue<TileFacade>(tilesPath);
        this.MoveToNextTile();
    }

    public override bool Process(SkillInput input)
    {
        if(this.nextTile != null)
        {
            Transform transform = input.Caster.transform;
            transform.position =  Vector3.MoveTowards(transform.position, nextTile.WorldPos, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, nextTile.WorldPos) < this.treshold)
            {
                bool result = this.MoveToNextTile();
                return !result;
            }
            else
            {
                return false;
            }
        }
        else
        {
            bool result = this.MoveToNextTile();
            return !result;
        }
    }

    public bool MoveToNextTile()
    {
        if(this.tilesPath.Count == 0)
        {
            this.nextTile = null;
            return false;
        }

        this.nextTile = this.tilesPath.Dequeue();
        return true;
    }
}