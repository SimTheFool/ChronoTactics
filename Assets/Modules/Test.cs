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
            /* this.foundPath = true;

            Cell[] cells = Resources.FindObjectsOfTypeAll<Cell>();

            Cell startCell = null;
            Cell endCell = null;

            for(int i = 0; i < cells.Length; i++)
            {
                if(cells[i].IsWalkable())
                {
                    startCell = cells[i];
                    break;
                }
            }

            for(int i = cells.Length - 3; i >= 0; i--)
            {
                Debug.Log(cells[i]);

                if(cells[i].IsWalkable())
                {
                    endCell = cells[i];
                    break;
                }
            }

            startCell.GetComponent<SpriteRenderer>().color = new Color(250, 0, 0, 1);
            endCell.GetComponent<SpriteRenderer>().color = new Color(250, 0, 0, 1); */

            /* List<Cell> path = startCell.findPath(endCell);

            foreach(Cell cell in path)
            {
                cell.GetComponent<SpriteRenderer>().color = new Color(0, 0, 250, 1);
            } */
        }
    }
}
