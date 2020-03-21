using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManhattanTopology : AbstractTopology
{

    public ManhattanTopology(Dictionary<Vector2Int, Cell> grid) : base(grid)
    {

    }

    public override int Distance(Vector2Int origin, Vector2Int dest)
    {
        Vector2Int distVect = dest - origin;
        return Mathf.Abs(distVect.x) + Mathf.Abs(distVect.y);
    }

    public override List<Vector2Int> ReverseDistance(Vector2Int origin, int distance)
    {
        List<Vector2Int> destinations = new List<Vector2Int>();
        for(int x = 0; x <= distance; x++)
        {
            int y =  distance - x;
            destinations.Add(new Vector2Int(origin.x + x, origin.y + y));
            destinations.Add(new Vector2Int(origin.x - x, origin.y + y));
            destinations.Add(new Vector2Int(origin.x + x, origin.y - y));
            destinations.Add(new Vector2Int(origin.x - x, origin.y - y));
        }

        return destinations;
    }

    public override bool IsWalkable(Cell cell)
    {
        return cell.getPrototype().isWalkable;
    }

    public override bool AreSameCell(Cell origin, Cell dest)
    {
        return origin.getCoord() == dest.getCoord();
    }

}
