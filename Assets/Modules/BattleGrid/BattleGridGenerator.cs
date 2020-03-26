using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGridGenerator : MonoBehaviour
{
    public GameObject cellPrefab;
    public GridPrototype gridPrototype;

    public void Start()
    {
        this.GenerateGrid(gridPrototype);
    }

    private void GenerateGrid(GridPrototype gridProto)
    {
        foreach(KeyValuePair<Vector2Int, CellPrototype> kvp in gridProto.grid)
        {
            int x = kvp.Key.x;
            int y = kvp.Key.y;
            CellPrototype prototype = kvp.Value;

            GameObject go = Instantiate(this.cellPrefab, new Vector2(x, y), Quaternion.identity);
            go.name = "Cell";
            go.transform.parent = this.gameObject.transform;

            Cell cell = go.GetComponent<Cell>();
            cell.Initialize(prototype, new Vector2Int(x, y)); 
        }
    }

}
