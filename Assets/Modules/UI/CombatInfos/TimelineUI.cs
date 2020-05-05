using UnityEngine.UI;
using System.Collections.Generic;

public class TimelineUI : UIChildrenGenerator
{
    private TimelineController timelineController;

    void Start()
    {
        this.timelineController = DependencyLocator.getTimelineController();
    }

    void Update()
    {
        Dictionary<int, List<ITimelineAgent>> agentsPerTurn = this.timelineController.RemainingAgentsPerTurn;

        this.Paint<KeyValuePair<int, List<ITimelineAgent>>>(agentsPerTurn, (turnUI, kvp) => {
            turnUI.GetComponent<TurnUI>().Paint<ITimelineAgent>(kvp.Value, (agentUI, agent) => {
                agentUI.GetComponent<Text>().text = agent.Name;
            });
        });
    }
}
