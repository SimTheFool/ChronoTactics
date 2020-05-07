using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapIdentifier : MonoBehaviour
{
    [SerializeField]
    private Tilemap groundMap = null;
    public Tilemap GroundMap => this.groundMap;

    [SerializeField]
    private Tilemap startMap = null;
    public Tilemap StartMap => this.startMap;
}