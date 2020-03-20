using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMapGenerator : MonoBehaviour
{
    private Dictionary<string, CellPrototype> cellPrototypes;
    public GameObject cellPrefab;

    public void Start()
    {
        this.cellPrototypes = ResourcesLoader.getCellPrototypes();
        this.GenerateGrid(15, 15);
    }

    private void GenerateGrid(int width, int height)
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                GameObject go = Instantiate(this.cellPrefab, new Vector2(x, y), Quaternion.identity);
                go.name = "Cell";
                go.transform.parent = this.gameObject.transform;

                Cell cell = go.GetComponent<Cell>();
                CellPrototype prototype = (Random.Range(0, 14) == 0) ? this.cellPrototypes["wall"] : this.cellPrototypes["floor"];
                cell.Initialize(prototype, new Vector2Int(x, y));                
            }
        }
    }

}
