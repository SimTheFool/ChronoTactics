using UnityEngine.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "StartTile", menuName = "Tiles/StartTiles", order = 1)]
public class SpecialTile : Tile
{
    public bool startTile = true;
}
