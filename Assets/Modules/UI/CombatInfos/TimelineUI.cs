using UnityEngine.UI;
using System.Collections.Generic;

public class TimelineUI : UIChildrenGenerator
{
    private TimelineHandler timelineHandler;

    void Start()
    {
        this.timelineHandler = DependencyLocator.getTimelineHandler();
    }

    void Update()
    {
        Dictionary<int, List<TimelineAgent>> agentsPerTurn = this.timelineHandler.RemainingAgentsPerTurn;

        this.Paint<KeyValuePair<int, List<TimelineAgent>>>(agentsPerTurn, (turnUI, kvp) => {
            turnUI.GetComponent<TurnUI>().Paint<TimelineAgent>(kvp.Value, (agentUI, agent) => {
                agentUI.GetComponent<Text>().text = agent.Name;
            });
        });
    }
}
