using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TilemapFacade : MonoBehaviour
{
    public Dictionary<Vector2Int, TileFacade> tilesMap = new Dictionary<Vector2Int, TileFacade>();

    public Tilemap tilemap;
    public BoundsInt bounds;

    private void Awake()
    {
        this.tilemap = this.GetComponent<Tilemap>();
    }

    private void Start()
    {
        for(int x = this.bounds.position.x; x < this.bounds.position.x + this.bounds.size.x; x++)
        {
            for(int y = this.bounds.position.y; y < this.bounds.position.y + this.bounds.size.y; y++)
            {
                TileBase tileBase = this.tilemap.GetTile(new Vector3Int(x, y, 0));

                if(tileBase == null)
                {
                    continue;
                }

                TileFacade tile = new TileFacade(new Vector2Int(x, y), tileBase);
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
        Vector2Int coord = (Vector2Int)this.tilemap.WorldToCell(pos);
        return this.GetTileFromCoord(coord);
    }
}