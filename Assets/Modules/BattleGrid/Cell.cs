using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour
{
    static private Dictionary<Vector2, Cell> grid = new Dictionary<Vector2, Cell>();

    private CellPrototype prototype;
    public CellPrototype getPrototype()
    {
        return this.prototype;
    }
    public void setPrototype(CellPrototype prototype)
    {
        this.prototype = prototype;
    }

    private Vector2 coord;
    public Vector2 getCoord()
    {
        return this.coord;
    }

    public void Initialize(CellPrototype prototype, Vector2 coord)
    {
        this.prototype = prototype;
        this.coord = coord;

        Cell.grid.Add(coord, this);
        this.GetComponent<SpriteRenderer>().sprite = this.prototype.sprite;
    }

    public bool IsWalkable()
    {
        return this.prototype.isWalkable;
    }

    public List<Cell> findWalkablePathTo(Cell dest)
    {
        //@TODO
        return new List<Cell>();
    }
}
