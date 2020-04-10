using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITopology
{
    List<Vector2Int> ReverseDistance(Vector2Int origin, int distance);
    int Distance(Vector2Int origin, Vector2Int dest);
}
