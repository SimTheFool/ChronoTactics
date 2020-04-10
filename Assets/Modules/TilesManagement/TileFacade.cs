using UnityEngine;
using UnityEngine.Tilemaps;

public class TileFacade : IPositionnable
{
    private Vector2Int coord;
    private TileBase tileBase;

    public Vector2Int Coord
    {
        get
        {
            return this.coord;
        }
    }

    public TileBase TileBase
    {
        get
        {
            return this.tileBase;
        }
    }

    public TileFacade(Vector2Int coord, TileBase tileBase)
    {
        this.coord = coord;
        this.tileBase = tileBase;
    }
}