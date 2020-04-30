using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;

public class TilemapFacade : MonoBehaviour
{
    public Dictionary<Vector2Int, TileFacade> tilesMap = new Dictionary<Vector2Int, TileFacade>();

    [SerializeField]
    private Tilemap groundMap = null;
    [SerializeField]
    private Tilemap specialMap = null;
    [SerializeField]
    private BoundsInt bounds;

    private bool initFlag = false;
    private List<TilemapAgent> agentsToLocateOnStart = new List<TilemapAgent>();

    private void Start()
    {
        for(int x = this.bounds.position.x; x < this.bounds.position.x + this.bounds.size.x; x++)
        {
            for(int y = this.bounds.position.y; y < this.bounds.position.y + this.bounds.size.y; y++)
            {
                GroundTile groundTile = (GroundTile)this.groundMap.GetTile(new Vector3Int(x, y, 0));
                SpecialTile specialTile = (SpecialTile)this.specialMap.GetTile(new Vector3Int(x, y, 0));

                if(groundTile == null)
                {
                    continue;
                }

                Vector2Int coord = new Vector2Int(x, y);
                TileFacade tile = new TileFacade(coord, this.GetWorldPosFromCoord(coord), groundTile, specialTile);
                this.tilesMap.Add(new Vector2Int(x, y), tile);
            }
        }

        this.initFlag = true;

        foreach(TilemapAgent agent in this.agentsToLocateOnStart)
        {
            this.LocateAgentOnStartingTile(agent);
        }
    }
    public TileFacade GetTileFromCoord(Vector2Int coord)
    {
        TileFacade tile;

        if(this.tilesMap.TryGetValue(coord, out tile))
        {
            return tile;
        }
        return null;
    }

    public TileFacade GetTileFromWorldPos(Vector3 pos)
    {
        Vector2Int coord = (Vector2Int)this.groundMap.WorldToCell(pos);
        return this.GetTileFromCoord(coord);
    }

    public TileFacade GetOneStartingTile()
    {
        return this.tilesMap.Select(kvp => kvp.Value).Where(tile => tile.SpecialTile != null && tile.Agent == null).FirstOrDefault();
    }

    public Vector3 GetWorldPosFromCoord(Vector2Int coord)
    {
        return this.groundMap.GetCellCenterWorld((Vector3Int)coord);
    }

    public void LocateAgentOnStartingTile(TilemapAgent agent)
    {
        TileFacade startingTile = this.GetOneStartingTile();

        if(this.initFlag)
        {
            agent.SetTile(startingTile);
            startingTile.Agent = agent;
            agent.transform.position = startingTile.WorldPos;
            return;
        }

        this.agentsToLocateOnStart.Add(agent);
    }
}