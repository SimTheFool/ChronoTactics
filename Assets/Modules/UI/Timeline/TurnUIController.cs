using UnityEngine;
using System.Collections.Generic;

public class TurnUIController : UIChildrenGenerator<AgentUIController>
{
    public void RenderAgents(List<ITimelineAgent> agents)
    {
        this.Paint<ITimelineAgent>(agents, (agentUI, agent) => {
            agentUI.SetAgentName(agent.Name);
        });
    }
}
