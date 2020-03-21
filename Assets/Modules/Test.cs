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

            Cell[] cells = Resources.FindObjectsOfTypeAll<Cell>();

            Cell startCell = null;
            Cell endCell = null;

            for(int i = 17; i < cells.Length; i++)
            {
                if(cells[i].IsWalkable(Cell.TopologyType.Manhattan))
                {
                    startCell = cells[i];
                    break;
                }
            }

            for(int i = cells.Length - 17; i >= 0; i--)
            {
                if(cells[i].IsWalkable(Cell.TopologyType.Manhattan))
                {
                    endCell = cells[i];
                    break;
                }
            }

            List<Cell> path = startCell.findPathTo(endCell, Cell.TopologyType.Manhattan);

            if(path != null)
            {
                foreach(Cell cell in path)
                {
                    cell.changeColor(Color.magenta);
                }
            }

            startCell.changeColor(Color.red);
            endCell.changeColor(Color.red);
        }
    }
}
