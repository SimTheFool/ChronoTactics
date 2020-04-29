using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class UIChildrenGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject uIPrefab = null;

    private List<GameObject> activeUIs = new List<GameObject>();
    private static Dictionary<int, List<GameObject>> uIsPools = new Dictionary<int, List<GameObject>>();

    protected List<GameObject> GetUIsPool()
    {
        int id = uIPrefab.GetInstanceID();
        List<GameObject> uIsPool;

        if(uIsPools.TryGetValue(id, out uIsPool))
        {
            return uIsPool;
        }

        uIsPools.Add(id, new List<GameObject>());
        return uIsPools[id];
    }

    private void DisableUI(int i)
    {
        GameObject uI = this.activeUIs[i];
        this.activeUIs.RemoveAt(i);
        uI.SetActive(false);
        this.GetUIsPool().Add(uI);
    }

    private void ActivateOrCreateUI(int i)
    {
        GameObject uI;
        List<GameObject> uIsPool = this.GetUIsPool();

        if(uIsPool.Count == 0)
        {
            uI = Instantiate(this.uIPrefab); 
        }
        else
        {
            uI = uIsPool[0];
            uIsPool.RemoveAt(0);
            uI.SetActive(true);
        }

        uI.transform.SetParent(this.gameObject.transform);
        this.activeUIs.Add(uI);
        Canvas.ForceUpdateCanvases();
    }

    public void Paint<TElement>(IEnumerable<TElement> enumerable, Action<GameObject, TElement> setUICallback)
    {        
        if(enumerable == null || enumerable.Count() == 0) return;

        int length = enumerable.Count();

        for(int i = 0; i < Mathf.Max(length, this.activeUIs.Count); i++)
        {
            if(i >= length)
            {
                this.DisableUI(i);
                continue;
            }

            if(i >= this.activeUIs.Count)
            {
                this.ActivateOrCreateUI(i);
            }

            TElement elem  = enumerable.ElementAt(i);
            GameObject uI = this.activeUIs[i];
            setUICallback(uI, elem);
        }   
    }
}