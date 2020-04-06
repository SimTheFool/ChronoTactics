using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Finder<TConcretePositionnable> where TConcretePositionnable: Object, IPositionnable
{
    private Dictionary<Vector2Int, TConcretePositionnable> map;

    // Links each T element with their cost for pathfinding algorithm.
    private class Node
    {
        private static int globalIncrement = 0;

        public TConcretePositionnable elem;
        public Node previous;
        public int f;
        public int h;
        public int uid;

        public Node(TConcretePositionnable elem, Node previous = null, int f = 0, int h = 0)
        {
            Node.globalIncrement++;

            this.elem = elem;
            this.previous = previous;
            this.f = f;
            this.h = h;
            this.uid = Node.globalIncrement;
        }
    }

    // Comparer used by the SortedList in pathfinding algorithm.
    private class ByHCost : IComparer<Node>
    {
        public int Compare(Node n1, Node n2)
        {
            if(n1.elem == n2.elem)
            {
                return 0;
            }

            int result = n1.h.CompareTo(n2.h);
            result = (result == 0) ? n2.f.CompareTo(n1.f) : result;
            result = (result == 0) ? n1.uid.CompareTo(n2.uid) : result;
            return result;
        }
    }

    public Finder(Dictionary<Vector2Int, TConcretePositionnable> map = null)
    {
        this.map = map;
    }

    public HashSet<TConcretePositionnable> GetElementsInRange(TConcretePositionnable origin, int rangeMin, int rangeMax, ITopology topology)
    {
        HashSet<TConcretePositionnable> elems = new HashSet<TConcretePositionnable>();

        for(int distance = rangeMin; distance <= rangeMax; distance++)
        {
            List<Vector2Int> positions = topology.ReverseDistance(origin.Coord, distance);

            foreach(Vector2Int pos in positions)
            {
                TConcretePositionnable elem;
                if(this.map.TryGetValue(pos, out elem))
                {
                    elems.Add(elem);
                }
            }
        }

        return elems;
    }

    public HashSet<TConcretePositionnable> GetFilteredElementsInRange(TConcretePositionnable origin, int rangeMin, int rangeMax, ITopology topology, IFilter filter)
    {
        HashSet<TConcretePositionnable> elems = new HashSet<TConcretePositionnable>();
        foreach(TConcretePositionnable elem in this.GetElementsInRange(origin, rangeMin, rangeMax,topology))
        {
            if(filter.IsAccessible(elem))
            {
                elems.Add(elem);
            }
        }

        return elems;
    }


    public int GetDistanceBetween(TConcretePositionnable elemA, TConcretePositionnable elemB, ITopology topology)
    {
        return topology.Distance(elemA.Coord, elemB.Coord);
    }

    // A-star pathfinding algorithm.
    public HashSet<TConcretePositionnable> findPath(TConcretePositionnable origin, TConcretePositionnable dest, ITopology topology, IFilter filter)
    {
        // Nodes which must be evaluated.
        SortedSet<Node> openSet = new SortedSet<Node>(new ByHCost());

        // Nodes already evaluated.
        List<Node> closedSet = new List<Node>();

        // The origin cell is the first node which will be evaluated.
        openSet.Add(new Node(origin, null, 0, this.GetDistanceBetween(origin, dest, topology)));

        // We proceed while there are still cells to be evaluated.
        while(openSet.Count > 0)
        {                   
            // We extract the best candidate of the openSet.
            Node currentNode = openSet.Min;
            openSet.Remove(currentNode);

            // If this candidate is the destination IPositionnable, we have found a path and we can end the algorithm.
            if(this.GetDistanceBetween(currentNode.elem, dest, topology) == 0)
            {
                // Rebuilding the final path and returning the list of cells.
                HashSet<TConcretePositionnable> finalSet = new HashSet<TConcretePositionnable>();
                Node tempNode =  closedSet[closedSet.Count - 1];

                while(tempNode != null)
                {
                   finalSet.Add(tempNode.elem);
                   tempNode = tempNode.previous;
                }

                return finalSet;
            }

            // We get all the IPositionnables in range of the candidate, and we determine if they are accessible.
            HashSet<TConcretePositionnable> elemsInRange = this.GetFilteredElementsInRange(currentNode.elem, 1, 1, topology, filter);
            foreach(TConcretePositionnable elemInRange in elemsInRange)
            {
                // If this IPositionnable has already been evaluated, we don't add it to candidate list.
                if(closedSet.Exists(n => n.elem == elemInRange))
                {
                    continue;
                }

                // We create a new node with that cell.
                int f = currentNode.f + 1;
                int h = f + this.GetDistanceBetween(elemInRange, dest, topology);
                Node candidateNode = new Node(elemInRange, currentNode, f, h);

                // We check if the node is already in the candidate list.
                if(!openSet.Any(n => n.elem == candidateNode.elem))
                {
                    // If it doesn't exist, we add that node to the candidate list.
                    openSet.Add(candidateNode);
                }
                else
                {
                    // If it exists, we try to remove it if it has a worse f cost. In that case, we had the new version of the node.
                    int removedNode = openSet.RemoveWhere((Node n) => {
                        bool isSame = n.elem == candidateNode.elem;
                        bool hasWorseCost = n.f > candidateNode.f;
                        return isSame && hasWorseCost;
                    });

                    if(removedNode > 0)
                    {
                        openSet.Add(candidateNode);
                    }
                }
            }

            closedSet.Add(currentNode);
        }
        return null;
    }
}
