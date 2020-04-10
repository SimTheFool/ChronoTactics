using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;

public class DebugPathfinding<TConcretePositionnable> where TConcretePositionnable: class, IPositionnable
{
    private enum NodeType
    {
        Closed,
        Open,
        Current
    }

    private class GizmoDatas
    {
        public NodeType type;
        public List<int> hCosts = new List<int>();
        public List<int> fCosts = new List<int>();

        public GizmoDatas(NodeType type, int h, int f)
        {
            this.type = type;
            this.hCosts.Add(h);
            this.fCosts.Add(f);
        }
    }

    private List<KeyValuePair<NodeType, PathfindingNode<TConcretePositionnable>>> nodes = new List<KeyValuePair<NodeType, PathfindingNode<TConcretePositionnable>>>();
    private Dictionary<Vector2Int, GizmoDatas> gizmosDatas = new Dictionary<Vector2Int, GizmoDatas>();
    private Finder<TConcretePositionnable> finder;
    private DebugPathfindingComponent debugComponent;

    public DebugPathfinding(Finder<TConcretePositionnable> finder)
    {
        GameObject debug = new GameObject("debug pathfinding");
        this.debugComponent = debug.AddComponent<DebugPathfindingComponent>();
        this.finder = finder;

        this.finder.OnAddToOpenSet += this.AddToOpenSetListener;
        this.finder.OnAddToClosedSet += this.AddToClosedSetListener;
        this.finder.OnNewCurrentNode += this.NewCurrentNodeListener;
        this.finder.OnEndPathfinding += this.EndPathfindingListener;
        this.debugComponent.OnDrawingGizmos += this.DrawingGizmosListener;
    }

    private void AddToOpenSetListener(PathfindingNode<TConcretePositionnable> node)
    {
        nodes.Add(new KeyValuePair<NodeType, PathfindingNode<TConcretePositionnable>>(NodeType.Open, node));
    }

    private void AddToClosedSetListener(PathfindingNode<TConcretePositionnable> node)
    {
        nodes.Add(new KeyValuePair<NodeType, PathfindingNode<TConcretePositionnable>>(NodeType.Closed, node));
    }

    private void NewCurrentNodeListener(PathfindingNode<TConcretePositionnable> node)
    {
        nodes.Add(new KeyValuePair<NodeType, PathfindingNode<TConcretePositionnable>>(NodeType.Current, node));
    }

    private void EndPathfindingListener()
    {
        this.finder.OnAddToOpenSet -= this.AddToOpenSetListener;
        this.finder.OnAddToClosedSet -= this.AddToClosedSetListener;
        this.finder.OnNewCurrentNode -= this.NewCurrentNodeListener;
        this.finder.OnEndPathfinding -= this.EndPathfindingListener;
        this.debugComponent.StartCoroutine(this.NodesToGizmoDatas());
    }

    private IEnumerator NodesToGizmoDatas()
    {
        for(int i = 0; i < this.nodes.Count; i++)
        {
            NodeType type = nodes[i].Key;
            PathfindingNode<TConcretePositionnable> node = nodes[i].Value;

            if(type == NodeType.Closed)
            {
                yield return new WaitForSeconds(1.5f);
            }

            GizmoDatas gizmoDatas;
            Vector2Int pos = node.elem.Coord;

            if(this.gizmosDatas.TryGetValue(pos, out gizmoDatas))
            {
                gizmoDatas.type = type;
                if(type != NodeType.Current)
                {
                    gizmoDatas.hCosts.Add(node.h);
                    gizmoDatas.fCosts.Add(node.f);
                }
            }
            else
            {
                gizmoDatas = new GizmoDatas(type, node.h, node.f);
                this.gizmosDatas.Add(pos, gizmoDatas);
            }
        }

        yield break;
    }

    private void DrawingGizmosListener()
    {
        foreach(KeyValuePair<Vector2Int, GizmoDatas> kvp in gizmosDatas)
        {
            GizmoDatas data = kvp.Value;
            Vector2Int pos = kvp.Key;

            switch(data.type)
            {
                case NodeType.Closed:
                    Gizmos.color = new Color(1, 0, 0, 0.5f);
                    break;
                case NodeType.Open:
                    Gizmos.color = new Color(0.2f, 0, 0.5f, 0.5f);
                    break;
                case NodeType.Current:
                    Gizmos.color = new Color(0, 1, 0, 0.5f);;
                    break;
                default:
                    Gizmos.color = new Color(0.2f, 0, 0.5f, 0.5f);
                    break;
            }

            Gizmos.DrawSphere(new Vector3(pos.x, pos.y, 0), 0.5f);

            #if UNITY_EDITOR
            string msg = "";
            for(int k = 0; k < data.hCosts.Count; k++)
            {
                msg += $"f: {data.fCosts[k]} h: {data.hCosts[k]} \n";
            }
            Handles.Label(new Vector3(pos.x - 0.5f, pos.y, 1), msg);
            # endif
        }
    }
}