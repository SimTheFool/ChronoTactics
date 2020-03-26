using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellPrototype", menuName = "ScriptableObjects/CellPrototype", order = 1)]
public class CellPrototype : ScriptableObject
{
    public string cellName;
    public Sprite sprite;
    public bool isWalkable = true;

    public override string ToString()
    {
        return this.cellName;
    }
}