using UnityEngine;

public class TileFacade : IPositionnable
{
    private TilemapAgent agent;
    public TilemapAgent Agent
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

    public Actor Actor
    {
        get
        {
            if(this.agent == null) return null;
            return this.agent.Actor;
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

    private SpecialTile specialTile;
    public SpecialTile SpecialTile
    {
        get
        {
            return this.specialTile;
        }
    }

    public TileFacade(Vector2Int coord, Vector3 worldPos, GroundTile groundTile, SpecialTile specialTile = null)
    {
        this.coord = coord;
        this.worldPos = worldPos;
        this.groundTile = groundTile;
        this.specialTile = specialTile;
    }
}