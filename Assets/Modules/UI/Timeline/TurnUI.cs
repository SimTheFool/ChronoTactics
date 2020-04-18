using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TurnUI : MonoBehaviour
{

    public GameObject agentUI;
    private List<GameObject> activeUIs = new List<GameObject>();
    private static List<GameObject> disabledUIs = new List<GameObject>();

    public List<TimelineAgent> agents = new List<TimelineAgent>();

    private void DisableUI(int i)
    {
        GameObject uI = this.activeUIs[i];
        this.activeUIs.RemoveAt(i);
        uI.SetActive(false);
        TurnUI.disabledUIs.Add(uI);
    }

    private void ActivateOrCreateUI(int i)
    {
        GameObject uI;

        if(TurnUI.disabledUIs.Count == 0)
        {
            uI = Instantiate(this.agentUI); 
        }
        else
        {
            uI = TurnUI.disabledUIs[0];
            TurnUI.disabledUIs.RemoveAt(0);
            uI.SetActive(true);
        }

        uI.transform.SetParent(this.gameObject.transform);
        this.activeUIs.Add(uI);
        Canvas.ForceUpdateCanvases();
    }

    private void SetUI(int i)
    {
        GameObject uI = this.activeUIs[i];
        TimelineAgent agent = this.agents[i];
        uI.GetComponent<Text>().text = agent.Name;
    }

    public void SetAgents(List<TimelineAgent> agents)
    {
        this.agents = agents;

        for (int i = 0; i < Mathf.Max(this.agents.Count, this.activeUIs.Count); i++)
        {
            if(i >= this.agents.Count)
            {
                this.DisableUI(i);
                continue;
            }

            if(i >= this.activeUIs.Count)
            {
                this.ActivateOrCreateUI(i);
            }

            this.SetUI(i);
        }
    }
}
