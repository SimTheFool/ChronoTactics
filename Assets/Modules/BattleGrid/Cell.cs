using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour
{
    private static Dictionary<Vector2Int, Cell> grid;

    public enum TopologyType { Walkable, Viewable };
    private static Dictionary<TopologyType, AbstractTopology> topologies;

    private CellPrototype prototype;
    private CellPrototype getPrototype()
    {
        return this.prototype;
    }
    private void setPrototype(CellPrototype prototype)
    {
        this.prototype = prototype;
        this.GetComponent<SpriteRenderer>().sprite = prototype.sprite;
    }

    private Vector2Int coord;
    private void setCoord(Vector2Int coord)
    {
        this.coord = coord;
    }
    public Vector2Int getCoord()
    {
        return this.coord;
    }

    public void Initialize(CellPrototype prototype, Vector2Int coord)
    {
        this.setPrototype(prototype);
        this.setCoord(coord);

        if(Cell.grid == null)
        {
            Cell.grid = new Dictionary<Vector2Int, Cell>();
        }
        Cell.grid.Add(coord, this);

        if(Cell.topologies == null)
        {
            Cell.topologies = new Dictionary<TopologyType, AbstractTopology>();
            //Cell.finderMap.Add(FinderType.Walkable, )
        }
    }

    public List<Cell> findPathTo(Cell dest, TopologyType type)
    {
        return Cell.topologies[type].findPath(this, dest);
    }
}
