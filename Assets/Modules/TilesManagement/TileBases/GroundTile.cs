using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEditor.Tilemaps;

public class GroundTile : Tile
{
    public bool isWalkable;

    [CreateTileFromPalette]
    public static TileBase CreateGroundTile(Sprite sprite)
    {
        var groundTile = ScriptableObject.CreateInstance<GroundTile>();
        groundTile.sprite = sprite;
        groundTile.name = sprite.name;
        return groundTile;
    }
}
