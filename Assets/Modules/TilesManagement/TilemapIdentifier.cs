using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapIdentifier : MonoBehaviour
{
    [SerializeField]
    private Tilemap groundMap;
    public Tilemap GroundMap => this.groundMap;

    [SerializeField]
    private Tilemap startMap;
    public Tilemap StartMap => this.startMap;
}