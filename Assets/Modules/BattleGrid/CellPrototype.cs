using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellPrototype", menuName = "ScriptableObjects/CellPrototype", order = 1)]
public class CellPrototype : ScriptableObject
{
    public string cellType;
    public Sprite sprite;
    public bool isWalkable = true;
}