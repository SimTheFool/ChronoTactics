using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private bool foundPath = false;

    /* public class Prop
    {

    }

    public class Pack
    {
        public int a;
        public Prop prop;
        public int uid;

        public Pack(int a, Prop prop)
        {
            this.a = a;
            this.prop = prop;
            this.uid = Random.Range(1, 10000);
        }
    }

    public class CompByA: IComparer<Pack>
    {
        public int Compare(Pack p1, Pack p2)
        {
            if(p1.prop == p2.prop)
            {
                return 0;
            }

            int result = p1.a.CompareTo(p2.a);
            result = (result == 0) ? p1.uid.CompareTo(p2.uid) : result;
            result = (result == 0) ? -1 : result;

            return result;
        }
    } */

    void Start()
    {
        /* SortedSet<Pack> sset = new SortedSet<Pack>(new CompByA());

        Prop someProp = new Prop();
        Pack pack1 = new Pack(1, someProp);
        Pack pack1a = new Pack(1, new Prop());
        Pack pack1b = new Pack(1, new Prop());
        
        Pack pack2 = new Pack(2, someProp);
        Pack pack2a = new Pack(2, new Prop());
        Pack pack2b = new Pack(2, new Prop());

        sset.Add(pack1);
        sset.Add(pack1a);
        sset.Add(pack1b);
        sset.Add(pack2a);
        sset.Add(pack2b);

        int removedNode = sset.RemoveWhere((Pack p) => {
            bool isSame = p.prop == pack2.prop;
            bool hasWorseA = p.a < pack2.a;

            Debug.Log(isSame && hasWorseA);
            return isSame && hasWorseA;
        });

        Debug.Log($"supprimé {removedNode}"); */
    }

    void Update()
    {
        if(!this.foundPath)
        {
            this.foundPath = true;

            Cell[] cells = Resources.FindObjectsOfTypeAll<Cell>();

            Cell startCell = cells[27];
            Cell endCell = cells[195];

            for(int i = 0; i < cells.Length; i++)
            {
                cells[i].thisIndex = i ;
            }

            startCell.findPathTo(endCell, Cell.TopologyType.CrowFly);

            /* if(path != null)
            {
                foreach(Cell cell in path)
                {
                    cell.changeColor(Color.magenta);
                }
            }

            startCell.changeColor(Color.red);
            endCell.changeColor(Color.red); */
        }
    }
}
