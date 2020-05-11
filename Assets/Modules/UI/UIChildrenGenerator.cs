using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class UIChildrenGenerator<TUIType> : MonoBehaviour where TUIType : MonoBehaviour
{
    [SerializeField]
    private TUIType uIPrefab = null;

    private List<TUIType> activeUIs = new List<TUIType>();
    private static Dictionary<int, List<TUIType>> uIsPools = new Dictionary<int, List<TUIType>>();

    protected List<TUIType> GetUIsPool()
    {
        int id = uIPrefab.GetInstanceID();
        List<TUIType> uIsPool;

        if(uIsPools.TryGetValue(id, out uIsPool))
        {
            return uIsPool;
        }

        uIsPools.Add(id, new List<TUIType>());
        return uIsPools[id];
    }

    private void DisableUI(int i)
    {
        TUIType uI = this.activeUIs[i];
        this.activeUIs.RemoveAt(i);
        uI.gameObject.SetActive(false);
        this.GetUIsPool().Add(uI);
        //Canvas.ForceUpdateCanvases();
    }

    private void ActivateOrCreateUI(int i)
    {
        TUIType uI;
        List<TUIType> uIsPool = this.GetUIsPool();

        if(uIsPool.Count == 0)
        {
            uI = Instantiate(this.uIPrefab);
        }
        else
        {
            uI = uIsPool[0];
            uIsPool.RemoveAt(0);
            uI.gameObject.SetActive(true);
        }

        uI.transform.SetParent(this.gameObject.transform);
        uI.transform.SetAsLastSibling();
        this.activeUIs.Add(uI);
        //Canvas.ForceUpdateCanvases();
    }

    public void Paint<TElement>(IEnumerable<TElement> enumerable, Action<TUIType, TElement> setUICallback)
    {        
        Canvas.ForceUpdateCanvases();
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
            TUIType uI = this.activeUIs[i];
            setUICallback(uI, elem);
        }
    }
}