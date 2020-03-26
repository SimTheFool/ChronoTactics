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

            Cell startCell = cells[203];
            Cell endCell = cells[146];

            for(int i = 0; i < cells.Length; i++)
            {
                cells[i].thisIndex = i ;
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
