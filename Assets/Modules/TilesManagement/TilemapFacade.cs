using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;

public class TilemapFacade : MonoBehaviour
{
    private Dictionary<Vector2Int, TileFacade> tilesMap = new Dictionary<Vector2Int, TileFacade>();
    public Dictionary<Vector2Int, TileFacade> TilesMap => this.tilesMap;

    private TilemapIdentifier tilemapIdentifier = null;
    private Tilemap GroundMap => this.tilemapIdentifier.GroundMap;
    private Tilemap StartMap => this.tilemapIdentifier.StartMap;
    private Tilemap MainMap => this.GroundMap;

    [SerializeField]
    private BoundsInt bounds;

    public void Init(List<ITilemapAgent> agents)
    {
        this.tilemapIdentifier = this.GetComponentInChildren<TilemapIdentifier>();
        this.InitTilesMap();

        foreach(ITilemapAgent agent in agents)
        {
            agent.SetTile(this.GetOneFreeStartingTile());
        }
    }

    private void InitTilesMap()
    {
        for(int x = this.bounds.position.x; x < this.bounds.position.x + this.bounds.size.x; x++)
        {
            for(int y = this.bounds.position.y; y < this.bounds.position.y + this.bounds.size.y; y++)
            {
                GroundTile groundTile = (GroundTile)this.GroundMap.GetTile(new Vector3Int(x, y, 0));
                if(groundTile == null) continue;  
                StartTile startTile = (StartTile)this.StartMap.GetTile(new Vector3Int(x, y, 0));

                
                Vector2Int coord = new Vector2Int(x, y);
                TileFacade tile = new TileFacade(coord, this.GetWorldPosFromCoord(coord), groundTile, startTile);
                this.tilesMap.Add(new Vector2Int(x, y), tile);
            }
        }
    }

    public Vector3 GetWorldPosFromCoord(Vector2Int coord)
    {
        return this.MainMap.GetCellCenterWorld((Vector3Int)coord);
    }

    public TileFacade GetOneFreeStartingTile()
    {
        return this.tilesMap.Select(kvp => kvp.Value).Where(tile => tile.StartTile != null && tile.Agent == null).FirstOrDefault();
    }

    public TileFacade GetTileFromCoord(Vector2Int coord)
    {
        TileFacade tile;

        if(this.tilesMap.TryGetValue(coord, out tile)) return tile;
        return null;
    }

    public TileFacade GetTileFromWorldPos(Vector3 pos)
    {
        Vector2Int coord = (Vector2Int)this.MainMap.WorldToCell(pos);
        return this.GetTileFromCoord(coord);
    }
}