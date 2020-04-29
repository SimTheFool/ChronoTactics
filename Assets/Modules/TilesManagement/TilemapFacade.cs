using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TilemapFacade : MonoBehaviour
{
    public Dictionary<Vector2Int, TileFacade> tilesMap = new Dictionary<Vector2Int, TileFacade>();

    public Tilemap groundMap;
    public Tilemap specialMap;
    public BoundsInt bounds;

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

    public TileFacade GetTileFromMousePos(Vector3 pos)
    {
        Vector2Int coord = (Vector2Int)this.groundMap.WorldToCell(pos);
        return this.GetTileFromCoord(coord);
    }

    public Vector3 GetWorldPosFromCoord(Vector2Int coord)
    {
        return this.groundMap.GetCellCenterWorld((Vector3Int)coord);
    }
}