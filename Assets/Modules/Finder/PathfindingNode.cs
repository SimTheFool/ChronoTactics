using UnityEngine;

public class PathfindingNode<TConcretePositionnable> where TConcretePositionnable: Object, IPositionnable
{
    private static int globalIncrement = 0;

    public TConcretePositionnable elem;
    public PathfindingNode<TConcretePositionnable> previous;
    public int f;
    public int h;
    public int uid;

    public PathfindingNode(TConcretePositionnable elem, PathfindingNode<TConcretePositionnable> previous = null, int f = 0, int h = 0)
    {
        PathfindingNode<TConcretePositionnable>.globalIncrement++;

        this.elem = elem;
        this.previous = previous;
        this.f = f;
        this.h = h;
        this.uid = PathfindingNode<TConcretePositionnable>.globalIncrement;
    }
}