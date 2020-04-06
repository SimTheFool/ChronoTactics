using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFilter
{
    bool IsAccessible(IPositionnable elem);
}
