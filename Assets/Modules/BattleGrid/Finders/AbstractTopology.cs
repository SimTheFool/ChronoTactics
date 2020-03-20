using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTopology
{
    private Dictionary<Vector2Int, Cell> grid;

    private struct Node
    {
        public Cell cell;
        public int f;
        public int h;

        public Node(Cell cell)
        {
            this.cell = cell;
            this.f = 0;
            this.h = 0;
        }
    }

    private class ByHCost : IComparer<Node>
    {
        public int Compare(Node origin, Node dest)
        {
            return origin.h.CompareTo(dest.h);
        }
    }

    public AbstractTopology(Dictionary<Vector2Int, Cell> grid)
    {
        this.grid = grid;
    }

    public List<Cell> findPath(Cell origin, Cell dest)
    {
        SortedSet<Node> openSet = new SortedSet<Node>(new ByHCost());
        List<Cell> closedSet = new List<Cell>();

        openSet.Add(new Node(origin));

        while(openSet.Count > 0)
        {
            Node currentNode = openSet.Min;
            openSet.Remove(currentNode);

            if(this.AreSameCell(currentNode.cell, dest))
            {
                // reconstituer chemin
            }

            List<Cell> accessibleCells = this.GetCellsInRange(currentNode.cell, 1, 1);

            foreach(Cell cell in accessibleCells)
            {
                if(!this.isWalkable(cell))
                {
                    continue;
                }

                if(closedSet.Contains(cell))
                {
                    continue;
                }

                foreach(Node node in openSet)
                {
                    // on check si la cellule est dans le set
                    // On compare leur cout f
                    // si le f de sorted set est inférieur on passe
                    // sinon on supprime l'élément du set
                }


            }

            //trouver le noeud le plus proche dans la liste ouverte
            //si ce ne est la destination, fini
            //sinon
        }

        return new List<Cell>();
    }

    public List<Cell> GetCellsInRange(Cell origin, int min, int max)
    {
        List<Cell> cells = new List<Cell>();
        int originX = origin.getCoord().x;
        int originY = origin.getCoord().y;

        for(int distance = min; distance <= max; distance++)
        {
            // To reverse distance, we have to assume we are in [0, inf]/[0, inf] to the destination in [0, inf]/[0, inf] at a specific distance.
            Vector2Int dest = this.ReverseDistance(new Vector2Int(Mathf.Abs(originX), Mathf.Abs(originY)), distance);

            // Because distance is symetric with x and y, we can deduce four dest which correspond to that specific distance.
            Cell cell;
            if(this.grid.TryGetValue(new Vector2Int(dest.x, dest.y), out cell))
            {
                cells.Add(cell);
            }

            if(this.grid.TryGetValue(new Vector2Int(dest.x * -1, dest.y), out cell))
            {
                cells.Add(cell);
            }

            if(this.grid.TryGetValue(new Vector2Int(dest.x, dest.y * -1), out cell))
            {
                cells.Add(cell);
            }

            if(this.grid.TryGetValue(new Vector2Int(dest.x * -1, dest.y * -1), out cell))
            {
                cells.Add(cell);
            }
        }

        return cells;
    }

    public int GetDistanceBetween(Cell origin, Cell dest)
    {
        return this.Distance(origin.getCoord(), dest.getCoord());
    }

    public abstract bool AreSameCell(Cell origin, Cell dest);

    public abstract bool isWalkable(Cell cell);

    // Here are the distance calculations for a specific topology.
    // Some reminder : those calcutions must be symetric with x-axis and y-axis.
    public abstract int Distance(Vector2Int origin, Vector2Int dest);

    public abstract Vector2Int ReverseDistance(Vector2Int origin, int distance);

}
