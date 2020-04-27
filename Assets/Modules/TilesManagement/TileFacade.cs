using UnityEngine;

public class TileFacade : IPositionnable
{
    private Vector2Int coord;
    public Vector2Int Coord
    {
        get
        {
            return this.coord;
        }
    }

    private GroundTile groundTile;
    public GroundTile GroundTile
    {
        get
        {
            return this.groundTile;
        }
    }

    private SpecialTile specialTile;
    public SpecialTile SpecialTile
    {
        get
        {
            return this.specialTile;
        }
    }

    public TileFacade(Vector2Int coord, GroundTile groundTile, SpecialTile specialTile = null)
    {
        this.coord = coord;
        this.groundTile = groundTile;
        this.specialTile = specialTile;
    }
}