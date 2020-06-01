using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PassesManager : TimelineComponentsBridge, ITimelineAgentsCollectionManager
{
    [SerializeField]
    private int previewedActorNb;

    private Pass linkedPasses = null;

    private ITimelineAgent currentAgent = null;

    public void Init(IEnumerable<ITimelineAgent> agents)
    {
        Dictionary<ITimelineAgent, int> agentsAtbs = agents.ToDictionary(agent => agent, agent => agent.Atb);
        this.linkedPasses = new Pass(agentsAtbs, 1);
    }

    public void MoveToNextAgent()
    {
        ITimelineAgent nextAgent = this.linkedPasses.GetAgentNextTo(this.currentAgent);

        if(nextAgent != null)
        {
            this.UpdateLinkedTurns();
            this.currentAgent = nextAgent;
            return;
        }

        Pass nextPass = this.linkedPasses.GetNextPass();

        if(nextPass == null)
        {
            this.currentAgent = null;
            return;
        }

        this.linkedPasses = nextPass;
        this.MoveToNextAgent();
    }

    public void Add(ITimelineAgent agent)
    {
        this.linkedPasses.AddOrUpdateAgent(agent, agent.Atb);
        this.UpdateLinkedTurns();
    }

    public void Remove(ITimelineAgent agent)
    {
        this.linkedPasses.RemoveAgent(agent);
        this.UpdateLinkedTurns();
    }

    private void UpdateLinkedTurns()
    {
        /* while(this.linkedTurns.RemainingAgentsCount < this.previewedAgentNb)
        {
            this.linkedTurns.NewTurn();
        }
        this.Events.OnPassesChange(this.linkedTurns.RemainingAgentsPerTurn, this.linkedTurns.CurrentAgent); */
    }

    /* [SerializeField]
    private int previewedAgentNb = 0;
    private Turn linkedTurns = null;

    public ITimelineAgent CurrentAgent => this.linkedTurns.CurrentAgent;
    public float CurrentPriorityScore => this.linkedTurns.CurrentPriorityScore;*/
}