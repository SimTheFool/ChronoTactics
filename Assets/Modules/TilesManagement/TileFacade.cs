using UnityEngine;

public class TileFacade : IPositionnable
{
    private ITilemapAgent agent;
    public ITilemapAgent Agent
    {
        get
        {
            return this.agent;
        }
        set
        {
            this.agent = value;
        }
    }


    private Vector2Int coord;
    public Vector2Int Coord
    {
        get
        {
            return this.coord;
        }
    }

    private Vector3 worldPos;
    public Vector3 WorldPos
    {
        get
        {
            return this.worldPos;
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

    private StartTile startTile;
    public StartTile StartTile
    {
        get
        {
            return this.startTile;
        }
    }

    public TileFacade(Vector2Int coord, Vector3 worldPos, GroundTile groundTile, StartTile startTile = null)
    {
        this.coord = coord;
        this.worldPos = worldPos;
        this.groundTile = groundTile;
        this.startTile = startTile;
    }
}