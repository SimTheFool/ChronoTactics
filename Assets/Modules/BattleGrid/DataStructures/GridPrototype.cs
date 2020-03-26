using UnityEngine;

public class GridPrototype : ScriptableObject
{
    public string gridName;
    public int width;
    public int height;

    [SerializeField]
    public CellPrototypeDictionary grid = new CellPrototypeDictionary();

    public override string ToString()
    {
        return this.gridName;
    }
}
