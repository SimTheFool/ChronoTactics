using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turn
{

    private Dictionary<ITimelineAgent, int> inputAgentsAtbs;
    private Dictionary<ITimelineAgent, int> outputAgentsAtbs;

    private SortedSet<KeyValuePair<ITimelineAgent, float>> agentsPriorities;
    private class CompareAgentPriorities : IComparer<KeyValuePair<ITimelineAgent, float>>
    {
        public int Compare(KeyValuePair<ITimelineAgent, float> p1, KeyValuePair<ITimelineAgent, float> p2)
        {
            if(p1.Key == p2.Key && p1.Value == p2.Value) return 0;            

            int result = p1.Value.CompareTo(p2.Value);

            result = (result == 0) ? p2.Key.Speed.CompareTo(p1.Key.Speed) : result;
            result = (result == 0) ? p1.Key.UniqId.CompareTo(p2.Key.UniqId) : result;
            return result;
        }
    }

    // We set a default pair of Agent/Priority, in a such a way it's lower than any other pair which may exists. Indeed, this pair is used to find next one in the priorities sorted list.
    private KeyValuePair<ITimelineAgent, float> currentAgentPriority = new KeyValuePair<ITimelineAgent, float>(null, -1);
    private int turnNb = 0;
    private Turn nextTurn = null;

    public Dictionary<int, List<ITimelineAgent>> RemainingAgentsPerTurn => this.GetRemainingAgentsPerTurn();
    public int RemainingAgentsCount => this.RemainingAgentsPerTurn.Aggregate(0, (acc, elem) => acc += elem.Value.Count);
    public ITimelineAgent CurrentAgent => this.currentAgentPriority.Key;
    public float CurrentPriorityScore => this.currentAgentPriority.Value;

    public Turn (Dictionary<ITimelineAgent, int> agentsAtbs, int turnNb)
    {
        this.inputAgentsAtbs = new Dictionary<ITimelineAgent, int>(agentsAtbs);
        this.outputAgentsAtbs = new Dictionary<ITimelineAgent, int>();
        this.agentsPriorities = new SortedSet<KeyValuePair<ITimelineAgent, float>>(new CompareAgentPriorities());
        this.turnNb = turnNb;

        foreach(ITimelineAgent agent in this.inputAgentsAtbs.Keys)
        {
            this.EvaluatePriorities(agent);
        }
    }

    private void EvaluatePriorities(ITimelineAgent agent)
    {
        int currentAtb = this.inputAgentsAtbs[agent];
        int finalAtb = currentAtb + agent.Speed;
        int canPlayNb = finalAtb / 100;

        this.agentsPriorities.RemoveWhere((KeyValuePair<ITimelineAgent, float> priority) => {
            return priority.Key == agent;
        });

        for(int i = 1; i <= canPlayNb; i++)
        {
            float priority = ((i * 100) - currentAtb) / (float)agent.Speed;
            this.agentsPriorities.Add(new KeyValuePair<ITimelineAgent, float>(agent, priority));
        }
        
        this.outputAgentsAtbs[agent] = finalAtb % 100;
    }

    public void AddOrUpdateAgent(ITimelineAgent agent, int atb)
    {
        this.inputAgentsAtbs[agent] = atb;
        this.EvaluatePriorities(agent);
        
        if(this.nextTurn == null)
            return;

        this.nextTurn.AddOrUpdateAgent(agent, this.outputAgentsAtbs[agent]);
    }

    public void RemoveAgent(ITimelineAgent agent)
    {
        this.inputAgentsAtbs.Remove(agent);
        this.outputAgentsAtbs.Remove(agent);
        this.agentsPriorities.RemoveWhere((KeyValuePair<ITimelineAgent, float> agentPriority) => {
            return agentPriority.Key == agent;
        });
        
        if(this.nextTurn == null)
            return;

        this.nextTurn.RemoveAgent(agent);
    }

    public void NewTurn()
    {
        if(this.outputAgentsAtbs.Count == 0)
            return;

        if(this.nextTurn != null)
        {
            this.nextTurn.NewTurn();
            return;
        }
            
        this.nextTurn = new Turn(this.outputAgentsAtbs, this.turnNb + 1);
    }

    public Turn MoveToNextTurn()
    {
        if(this.nextTurn == null || this.outputAgentsAtbs.Count <= 0)
            return null;

        foreach(KeyValuePair<ITimelineAgent, int> agentAtb in this.outputAgentsAtbs)
        {
            agentAtb.Key.Atb = agentAtb.Value;
        }

        return this.nextTurn;
    }

    public ITimelineAgent MoveToNextAgent()
    {
        List<KeyValuePair<ITimelineAgent, float>> agentsPriorities = this.GetAllAgentPrioritiesAfterCurrent();

        if(agentsPriorities.Count == 0) return null;

        this.currentAgentPriority = agentsPriorities[0];
        return this.currentAgentPriority.Key;
    }

    private List<KeyValuePair<ITimelineAgent, float>> GetAllAgentPrioritiesAfterCurrent()
    {
        List<KeyValuePair<ITimelineAgent, float>> result = new List<KeyValuePair<ITimelineAgent, float>>();
        KeyValuePair<ITimelineAgent, float> upperAgentPriority = new KeyValuePair<ITimelineAgent, float>(null, 1.1f);

        result = this.agentsPriorities.GetViewBetween(this.currentAgentPriority, upperAgentPriority).ToList();
        result.Remove(this.currentAgentPriority);

        return result;
    }

    private Dictionary<int, List<ITimelineAgent>> GetRemainingAgentsPerTurn()
    {
        Dictionary<int, List<ITimelineAgent>> agentsPerTurn = new Dictionary<int, List<ITimelineAgent>>();
        List<ITimelineAgent> agents = this.GetAllAgentPrioritiesAfterCurrent().Select((KeyValuePair<ITimelineAgent, float> agentPriority) => {
            return agentPriority.Key;
        }).ToList();

        agentsPerTurn[this.turnNb] = agents;

        if(this.nextTurn == null)
            return agentsPerTurn;

        foreach(KeyValuePair<int, List<ITimelineAgent>> kvp in this.nextTurn.GetRemainingAgentsPerTurn())
        {
            agentsPerTurn[kvp.Key] = kvp.Value;
        }

        return agentsPerTurn;
    }
}