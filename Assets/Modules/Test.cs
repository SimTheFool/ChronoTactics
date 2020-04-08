using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private bool foundPath = false;

    void Start()
    {

    }

    void Update()
    {
        if(!this.foundPath)
        {
            this.foundPath = true;

            Finder<Cell> pathfinder = DependencyLocator.getPathfinder();

            Cell[] cells = Resources.FindObjectsOfTypeAll<Cell>();

            Cell startCell = cells[27];
            Cell endCell = cells[195];

            for(int i = 0; i < cells.Length; i++)
            {
                cells[i].thisIndex = i ;
            }

            HashSet<Cell> path = pathfinder.FindPath_Debug(startCell, endCell, new ManhattanTopology(), new WalkableFilter());
        }
    }
}
