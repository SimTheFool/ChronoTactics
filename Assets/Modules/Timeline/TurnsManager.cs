using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TurnsManager : TimelineComponentsBridge
{
    [SerializeField]
    private int previewedAgentNb = 0;
    private Turn linkedTurns = null;

    public void Init(List<ITimelineAgent> agents)
    {
        Dictionary<ITimelineAgent, int> agentsAtbs = agents.ToDictionary(agent => agent, agent => agent.Atb);
        this.linkedTurns = new Turn(agentsAtbs, 1);
    }

    public ITimelineAgent GetNextAgent()
    {
        ITimelineAgent nextAgent = this.linkedTurns.MoveToNextAgent();

        if(nextAgent != null)
        {
            this.UpdateLinkedTurns();
            return nextAgent;
        }   

        Turn nextTurn = this.linkedTurns.MoveToNextTurn();

        if(nextTurn == null)
        {
            return null;
        }
            
        this.linkedTurns = nextTurn;  
        return this.GetNextAgent();
    }

    public void AddOrUpdateAgent(ITimelineAgent agent)
    {
        this.linkedTurns.AddOrUpdateAgent(agent, agent.Atb);
        this.UpdateLinkedTurns();
    }

    public void RemoveAgent(ITimelineAgent agent)
    {
        this.linkedTurns.RemoveAgent(agent);
        this.UpdateLinkedTurns();
    }

    private void UpdateLinkedTurns()
    {
        while(this.linkedTurns.RemainingAgentsCount < this.previewedAgentNb)
        {
            this.linkedTurns.NewTurn();
        }
        this.Events.OnTimelineChange(this.linkedTurns.RemainingAgentsPerTurn, this.linkedTurns.CurrentAgent);
    }
}