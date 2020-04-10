using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPositionnable
{
    Vector2Int Coord
    {
        get;
    }

    //List<IPositionnable> GetNonNaturalPositonnables(int dist);
}
