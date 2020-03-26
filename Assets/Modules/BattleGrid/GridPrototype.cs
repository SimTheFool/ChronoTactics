using UnityEngine;
using System.Collections.Generic;

public class GridPrototype : ScriptableObject
{
    public string gridName;
    public int width;
    public int height;

    [SerializeField]
    public CellDictionary grid = new CellDictionary();

    public override string ToString()
    {
        return this.gridName;
    }
}
