using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class AbstractTopology
{
    private Dictionary<Vector2Int, Cell> grid;

    // Links each cell with their cost for pathfinding algorithm.
    private class Node
    {
        private static int globalIncrement = 0;

        public Cell cell;
        public Node previous;
        public int f;
        public int h;
        public int uid;

        public Node(Cell cell, Node previous = null, int f = 0, int h = 0)
        {
            Node.globalIncrement++;

            this.cell = cell;
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
            if(n1.cell == n2.cell)
            {
                return 0;
            }

            int result = n1.h.CompareTo(n2.h);
            result = (result == 0) ? n2.f.CompareTo(n1.f) : result;
            result = (result == 0) ? n1.uid.CompareTo(n2.uid) : result;
            return result;
        }
    }

    public AbstractTopology(Dictionary<Vector2Int, Cell> grid)
    {
        this.grid = grid;
    }

    // A-star pathfinding algorithm.
    public IEnumerator findPath(Cell origin, Cell dest)
    {
        origin.changeColor(Color.red);
        dest.changeColor(Color.red);
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

            currentNode.cell.changeColor(Color.white);
            yield return new WaitForSeconds(0.2f);
            currentNode.cell.changeColor(Color.red);

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

                yield return null;
                //return finalSet;
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
                if(closedSet.Exists(n => n.cell == cellInRange))
                {
                    continue;
                }

                cellInRange.changeColor(Color.magenta);

                // We create a new node with that cell.
                int f = currentNode.f + 1;
                int h = f + this.GetDistanceBetween(cellInRange, dest);
                Node candidateNode = new Node(cellInRange, currentNode, f, h);

                // We check if the node is already in the candidate list.
                if(!openSet.Any(n => n.cell == candidateNode.cell))
                {
                    // If it doesn't exist, we add that node to the candidate list.
                    openSet.Add(candidateNode);
                }
                else
                {
                    // If it exists, we try to remove it if it has a worse f cost. In that case, we had the new version of the node.
                    int removedNode = openSet.RemoveWhere((Node n) => {
                        bool isSame = n.cell == candidateNode.cell;
                        bool hasWorseCost = n.f > candidateNode.f;
                        return isSame && hasWorseCost;
                    });

                    if(removedNode > 0)
                    {
                        openSet.Add(candidateNode);
                        cellInRange.fcost = f;
                    }
                }
            }

            closedSet.Add(currentNode);
            yield return new WaitForSeconds(0.7f);
        }
        yield return null;
    }

    public List<Cell> GetCellsInRange(Cell origin, int min, int max)
    {
        List<Cell> cells = new List<Cell>();
        int originX = origin.getCoord().x;
        int originY = origin.getCoord().y;

        for(int distance = min; distance <= max; distance++)
        {
            List<Vector2Int> destinations = this.ReverseDistance(new Vector2Int(originX, originY), distance);

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
