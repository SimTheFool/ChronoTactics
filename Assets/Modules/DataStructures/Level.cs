using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 0)]
public class Level : ScriptableObject
{
    public GameObject map;
    public List<GameObject> actors;
}