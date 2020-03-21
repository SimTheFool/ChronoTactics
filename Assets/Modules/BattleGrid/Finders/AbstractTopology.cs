using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTopology
{
    private Dictionary<Vector2Int, Cell> grid;

    // Links each cell with their cost for pathfinding algorithm.
    private class Node
    {
        public Cell cell;
        public Node previous;
        public int f;
        public int h;

        public Node(Cell cell, Node previous = null, int f = 0, int h = 0)
        {
            this.cell = cell;
            this.previous = previous;
            this.f = f;
            this.h = h;
        }
    }

    // Comparer used by the SortedList in pathfinding algorithm.
    private class ByHCost : IComparer<Node>
    {
        public int Compare(Node origin, Node dest)
        {
            int result = origin.h.CompareTo(dest.h);
            result = (result == 0) ? -1 : result;
            result = (origin.cell == dest.cell) ? 0 : result;
            return result;
        }
    }

    public AbstractTopology(Dictionary<Vector2Int, Cell> grid)
    {
        this.grid = grid;
    }

    // A-star pathfinding algorithm.
    public List<Cell> findPath(Cell origin, Cell dest)
    {
        // Node which must be evaluated.
        SortedSet<Node> openSet = new SortedSet<Node>(new ByHCost());

        // Node already evaluated.
        List<Node> closedSet = new List<Node>();

        // The origin cell is the first node which will be evaluated.
        openSet.Add(new Node(origin, null, 0, this.GetDistanceBetween(origin, dest)));

        // We proceed while there are still cells to be evaluated.
        while(openSet.Count > 0)
        {
            // We extract the best candidate of the openSet.
            Node currentNode = openSet.Min;
            openSet.Remove(currentNode);

            // If this candidate is the destination cell, we have found a path and we can end the algorithm.
            if(this.AreSameCell(currentNode.cell, dest))
            {
                // Rebuilding the final path and returning the list of cells.
                List<Cell> finalSet = new List<Cell>();
                Node tempNode =  closedSet[closedSet.Count - 1];

                while(tempNode != null)
                {
                   finalSet.Add(tempNode.cell);
                   tempNode = tempNode.previous;
                }

                return finalSet;
            }

            // We get all the cells in range of the candidate, and we determine if they are accessible.
            List<Cell> cellsInRange = this.GetCellsInRange(currentNode.cell, 1, 1);
            foreach(Cell cellInRange in cellsInRange)
            {
                // If this cell is not acessible, we don't add it to candidate list.
                if(!this.IsWalkable(cellInRange))
                {
                    continue;
                }

                // If this cell has already been evaluated, we don't add it to candidate list.
                if(closedSet.Exists(node => {
                    return node.cell == cellInRange;
                }))
                {
                    continue;
                }

                // We create a new node with that cell.
                int f = currentNode.f + 1;
                int h = f + this.GetDistanceBetween(cellInRange, dest);
                Node candidateNode = new Node(cellInRange, currentNode, f, h);

                // We check if the node is already in the candidate list.
                if(!openSet.Contains(candidateNode))
                {
                    // If it doesn't exist, we add that node to the candidate list.
                    openSet.Add(candidateNode);
                }
                else
                {
                    // If it exists, we try to remove it if it has a worse f cost. In that case, we had the new version of the node.
                    int removedNode = openSet.RemoveWhere((Node x) => {
                        return (x.f > candidateNode.f);
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

    public List<Cell> GetCellsInRange(Cell origin, int min, int max)
    {
        List<Cell> cells = new List<Cell>();
        int originX = origin.getCoord().x;
        int originY = origin.getCoord().y;

        for(int distance = min; distance <= max; distance++)
        {
            // To reverse distance, we have to assume we are in [0, inf]/[0, inf].
            List<Vector2Int> destinations = this.ReverseDistance(new Vector2Int(Mathf.Abs(originX), Mathf.Abs(originY)), distance);

            foreach(Vector2Int dest in destinations)
            {
                Cell cell;
                if(this.grid.TryGetValue(dest, out cell))
                {
                    if(!cells.Contains(cell))
                    {
                        cells.Add(cell);
                    }
                }
            }
        }

        return cells;
    }

    public int GetDistanceBetween(Cell origin, Cell dest)
    {
        return this.Distance(origin.getCoord(), dest.getCoord());
    }

    public abstract bool AreSameCell(Cell origin, Cell dest);

    public abstract bool IsWalkable(Cell cell);

    public abstract int Distance(Vector2Int origin, Vector2Int dest);

    public abstract List<Vector2Int> ReverseDistance(Vector2Int origin, int distance);

}
