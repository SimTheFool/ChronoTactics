using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillBarUI : MonoBehaviour
{
    public GameObject skillUI;
    private List<GameObject> activeUIs = new List<GameObject>();
    private List<GameObject> disabledUIs = new List<GameObject>();

    void Update()
    {
        Actor currentActor = DependencyLocator.getCurrentActor();
        List<KeyValuePair<string, UnityAction>> actions = (currentActor == null) ? null : currentActor.Actions;
        
        if(actions == null || actions.Count == 0) return;

        for(int i = 0; i < Mathf.Max(actions.Count, this.activeUIs.Count); i++)
        {
            if(i >= actions.Count)
            {
                this.DisableUI(i);
                continue;
            }

            if(i >= this.activeUIs.Count)
            {
                this.ActivateOrCreateUI(i);
            }

            KeyValuePair<string, UnityAction> kvp = actions.ElementAt(i);
            this.SetUI(i, kvp);
        }
    }

    private void DisableUI(int i)
    {
        GameObject uI = this.activeUIs[i];
        this.activeUIs.RemoveAt(i);
        uI.SetActive(false);
        this.disabledUIs.Add(uI);
    }

    private void ActivateOrCreateUI(int i)
    {
        GameObject uI;

        if(this.disabledUIs.Count == 0)
        {
            uI = Instantiate(this.skillUI); 
        }
        else
        {
            uI = this.disabledUIs[0];
            this.disabledUIs.RemoveAt(0);
            uI.SetActive(true);
        }

        uI.transform.SetParent(this.gameObject.transform);
        this.activeUIs.Add(uI);
        Canvas.ForceUpdateCanvases();
    }

    private void SetUI(int i, KeyValuePair<string, UnityAction> kvp)
    {
        GameObject uI = this.activeUIs[i];
        uI.GetComponentInChildren<Text>().text = kvp.Key;
        uI.GetComponent<Button>().onClick.RemoveAllListeners();
        uI.GetComponent<Button>().onClick.AddListener(kvp.Value);
    }
}
