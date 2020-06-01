using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PassesManager : TimelineComponentsBridge, ITimelineAgentsCollectionManager
{
    [SerializeField]
    private int previewedActorNb = 0;

    private Pass linkedPasses = null;

    private ITimelineAgent currentAgent = null;
    private float currentPriorityScore;

    public ITimelineAgent CurrentAgent => this.currentAgent;
    public float CurrentPriorityScore => this.currentPriorityScore;

    public void Init(IEnumerable<ITimelineAgent> agents)
    {
        Dictionary<ITimelineAgent, int> agentsAtbs = agents.ToDictionary(agent => agent, agent => agent.Atb);
        this.linkedPasses = new Pass(agentsAtbs, 1);
    }

    public void MoveToNextAgent()
    {
        (this.currentAgent, this.currentPriorityScore) = this.linkedPasses.GetAgentPriorityScoreNextTo(this.currentAgent, this.currentPriorityScore);

        if(this.currentAgent != null)
        {
            this.UpdateLinkedTurns();
            return;
        }

        this.linkedPasses = this.linkedPasses.GetNextPass();

        if(this.linkedPasses == null)
            return;

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
        Dictionary<int, List<ITimelineAgent>> actorsPerPass = this.linkedPasses.GetActorsPerPassNextTo(this.currentAgent, this.currentPriorityScore);

        while(actorsPerPass.Aggregate(0, (acc, passAgentActors) => acc + passAgentActors.Value.Count) < this.previewedActorNb)
        {
            this.linkedPasses.NewPass();
            actorsPerPass = this.linkedPasses.GetActorsPerPassNextTo(this.currentAgent, this.currentPriorityScore);
        }

        this.Events.OnPassesChange(actorsPerPass, this.CurrentAgent);
    }
}