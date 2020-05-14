using UnityEngine.UI;
using System.Collections.Generic;

public class TimelineUIObserver : UIChildrenGenerator<TurnUIController>
{
    private TimelineController timelineController;

    void Start()
    {
        this.timelineController = DependencyLocator.getTimelineController();
        this.timelineController.Events.onTimelineChange += this.RenderTurns;
    }

    private void RenderTurns(Dictionary<int, List<ITimelineAgent>> agentsPerTurn, ITimelineAgent agent)
    {
        this.Paint<KeyValuePair<int, List<ITimelineAgent>>>(agentsPerTurn, (turnUI, agents) => {
            turnUI.RenderAgents(agents.Value);
        });
    }
}
