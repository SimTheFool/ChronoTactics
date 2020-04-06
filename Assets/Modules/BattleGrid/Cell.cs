using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour, IPositionnable
{
    public static Dictionary<Vector2Int, Cell> grid;

    private CellPrototype prototype;
    public CellPrototype getPrototype()
    {
        return this.prototype;
    }
    private void setPrototype(CellPrototype prototype)
    {
        this.prototype = prototype;
        this.GetComponent<SpriteRenderer>().sprite = prototype.sprite;
    }

    private Vector2Int coord;

    public void Initialize(CellPrototype prototype, Vector2Int coord)
    {
        this.setPrototype(prototype);
        this.coord = coord;

        if(Cell.grid == null)
        {
            Cell.grid = new Dictionary<Vector2Int, Cell>();
        }
        Cell.grid.Add(coord, this);
    }

    public Vector2Int Coord
    {
        get
        {
            return this.coord;
        }
    }

    // @Debug For debug purposes.
    public void ChangeColor(Color color)
    {
        this.GetComponent<SpriteRenderer>().color = color;
    }
    public int thisIndex;
    public int hcost = 0;
    public int fcost = 0;
}
