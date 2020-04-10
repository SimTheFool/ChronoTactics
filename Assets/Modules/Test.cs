using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Test : MonoBehaviour
{

    private bool foundPath = false;
    public List<TileFacade> tiles = new List<TileFacade>();


    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TileFacade tile = DependencyLocator.getTilemap().GetTileFromMousePos(mouseWorldPos);

            if(tile != null)
            {
                this.tiles.Add(tile);
            }
        }

        if(!this.foundPath && this.tiles.Count >= 2)
        {
            this.foundPath = true;

            Finder<TileFacade> pathfinder = DependencyLocator.getPathfinder();
            TileFacade startTile = this.tiles[0];
            TileFacade endTile = this.tiles[1];

            HashSet<TileFacade> path = pathfinder.FindPath_Debug(startTile, endTile, new CrowFlyTopology(), new WalkableFilter());
        }
    }
}
