using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourcesLoader : MonoBehaviour
{

    private static ResourcesLoader instance;

    public string cellPrototypesPath;
    private Dictionary<string, CellPrototype> cellPrototypes;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public static Dictionary<string,CellPrototype> getCellPrototypes()
    {
        if(instance.cellPrototypes == null)
        {
            instance.cellPrototypes = new Dictionary<string, CellPrototype>();

            foreach(CellPrototype proto in Resources.LoadAll<CellPrototype>("Cell Prototypes"))
            {
                instance.cellPrototypes.Add(proto.cellName, proto);
            }
        }

        return instance.cellPrototypes;
    }
}
