using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TimelineUI : MonoBehaviour
{
    private TimelineHandler timelineHandler;

    public GameObject turnUI;

    private List<GameObject> activeUIs = new List<GameObject>();
    private List<GameObject> disabledUIs = new List<GameObject>();

    void Start()
    {
        this.timelineHandler = DependencyLocator.getTimelineHandler();
    }

    void Update()
    {
        //@Tips : Observer Pattern instead of checking data from timeline each frame.
        Dictionary<int, List<TimelineAgent>> agentsPerTurn = this.timelineHandler.RemainingAgentsPerTurn;
        if(agentsPerTurn == null) return;

        for(int i = 0; i < Mathf.Max(agentsPerTurn.Count, this.activeUIs.Count); i++)
        {
            if(i >= agentsPerTurn.Count)
            {
                this.DisableUI(i);
                continue;
            }

            if(i >= this.activeUIs.Count)
            {
                this.ActivateOrCreateUI(i);
            }

            KeyValuePair<int, List<TimelineAgent>> kvp = agentsPerTurn.ElementAt(i);
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
            uI = Instantiate(this.turnUI);
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

    private void SetUI(int i, KeyValuePair<int, List<TimelineAgent>> kvp)
    {
        GameObject uI = this.activeUIs[i];
        uI.GetComponent<TurnUI>().SetAgents(kvp.Value);
    }
}
