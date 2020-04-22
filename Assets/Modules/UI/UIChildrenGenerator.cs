using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class UIChildrenGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject uIPrefab;

    private List<GameObject> activeUIs = new List<GameObject>();
    private static Dictionary<string, List<GameObject>> pooledUIs = new Dictionary<string, List<GameObject>>();

    protected abstract List<GameObject> PooledUIs
    {
        get;
    }

    private void DisableUI(int i)
    {
        GameObject uI = this.activeUIs[i];
        this.activeUIs.RemoveAt(i);
        uI.SetActive(false);
        this.PooledUIs.Add(uI);
    }

    private void ActivateOrCreateUI(int i)
    {
        GameObject uI;

        if(this.PooledUIs.Count == 0)
        {
            uI = Instantiate(this.uIPrefab); 
        }
        else
        {
            uI = this.PooledUIs[0];
            this.PooledUIs.RemoveAt(0);
            uI.SetActive(true);
        }

        uI.transform.SetParent(this.gameObject.transform);
        this.activeUIs.Add(uI);
        Canvas.ForceUpdateCanvases();
    }

    protected void Paint<TElement>(IEnumerable<TElement> enumerable)
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
            this.SetUI<TElement>(i, elem);
        }   
    }

    protected abstract void SetUI<TElement>(int i, TElement elem);
}