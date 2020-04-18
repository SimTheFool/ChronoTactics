using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turn
{
    private Dictionary<TimelineAgent, int> inputAgentAtbs;
    private Dictionary<TimelineAgent, int> outputAgentAtbs;
    private SortedSet<KeyValuePair<TimelineAgent, float>> agentPriorities;
    private class CompareAgentPriorities : IComparer<KeyValuePair<TimelineAgent, float>>
    {
        public int Compare(KeyValuePair<TimelineAgent, float> p1, KeyValuePair<TimelineAgent, float> p2)
        {
            if(p1.Key == p2.Key && p1.Value == p2.Value)
            {
                return 0;
            }

            int result = p1.Value.CompareTo(p2.Value);
            result = (result == 0) ? p2.Key.Speed.CompareTo(p1.Key.Speed) : result;
            result = (result == 0) ? p1.Key.Name.CompareTo(p2.Key.Name) : result;
            return result;
        }
    }


    private KeyValuePair<TimelineAgent, float> currentAgentPriority = new KeyValuePair<TimelineAgent, float>(null, -1);
    private int turnNb = 0;

    private Turn nextTurn = null;
    public Turn NextTurn
    {
        get
        {
            return this.nextTurn;
        }
    }

    private Dictionary<int, List<TimelineAgent>> remainingAgentsPerTurn;
    private bool refreshRemainingAgentsPerTurn = true;
    public Dictionary<int, List<TimelineAgent>> RemainingAgentsPerTurn
    {
        get
        {
            if(this.refreshRemainingAgentsPerTurn)
            {
                this.remainingAgentsPerTurn = this.GetRemainingAgentsPerTurn();
                this.refreshRemainingAgentsPerTurn = false;
            }

            return this.remainingAgentsPerTurn;
        }
    }

    public int Count
    {
        get
        {
            int count = this.RemainingAgentsPerTurn.Count;
            count = (this.nextTurn == null) ? count : count + this.nextTurn.RemainingAgentsPerTurn.Count;
            return count;
        }
    }


    public Turn (HashSet<TimelineAgent> agents, int turnNb = 1)
    {
        this.inputAgentAtbs = new Dictionary<TimelineAgent, int>();
        this.turnNb = turnNb;
        foreach(TimelineAgent agent in agents)
        {
            this.inputAgentAtbs.Add(agent, agent.Atb);
        }
        this.Initialize();
    }

    public Turn (Dictionary<TimelineAgent, int> atbs, int turnNb = 1)
    {
        this.inputAgentAtbs = new Dictionary<TimelineAgent, int>(atbs);
        this.turnNb = turnNb;
        this.Initialize();
    }

    private void Initialize()
    {
        this.agentPriorities = new SortedSet<KeyValuePair<TimelineAgent, float>>(new CompareAgentPriorities());
        this.outputAgentAtbs = new Dictionary<TimelineAgent, int>();
        foreach(TimelineAgent agent in this.inputAgentAtbs.Keys)
        {
            this.EvaluatePriorities(agent);
        }
    }

    public void AddOrUpdateAgent(TimelineAgent agent, int atb = -1)
    {
        this.refreshRemainingAgentsPerTurn = true;

        this.inputAgentAtbs[agent] = (atb < 0) ? agent.Atb : atb;
        this.EvaluatePriorities(agent);
        
        if(this.nextTurn == null) return;

        this.nextTurn.AddOrUpdateAgent(agent, this.outputAgentAtbs[agent]);
    }

    public void RemoveAgent(TimelineAgent agent)
    {
        this.refreshRemainingAgentsPerTurn = true;

        this.inputAgentAtbs.Remove(agent);
        this.outputAgentAtbs.Remove(agent);
        this.agentPriorities.RemoveWhere((KeyValuePair<TimelineAgent, float> agentPriority) => {
            return agentPriority.Key == agent;
        });
        
        if(this.nextTurn == null) return;

        this.nextTurn.RemoveAgent(agent);
    }

    private void EvaluatePriorities(TimelineAgent agent)
    {
        int currentAtb = this.inputAgentAtbs[agent];
        int finalAtb = currentAtb + agent.Speed;
        int canPlayNb = finalAtb / 100;

        this.agentPriorities.RemoveWhere((KeyValuePair<TimelineAgent, float> priority) => {
            return priority.Key == agent;
        });

        for(int i = 1; i <= canPlayNb; i++)
        {
            float priority = ((i * 100) - currentAtb) / (float)agent.Speed;
            this.agentPriorities.Add(new KeyValuePair<TimelineAgent, float>(agent, priority));
        }
        
        this.outputAgentAtbs[agent] = finalAtb % 100;
    }

    public void NewTurn()
    {
        this.refreshRemainingAgentsPerTurn = true;

        if(this.nextTurn == null)
        {
            this.nextTurn = new Turn(this.outputAgentAtbs, this.turnNb + 1);
            return;
        }

        this.nextTurn.NewTurn();
    }

    public Turn MoveToNextTurn()
    {
        if(this.nextTurn == null || this.outputAgentAtbs.Count <= 0) return null;

        foreach(KeyValuePair<TimelineAgent, int> agentAtb in this.outputAgentAtbs)
        {
            agentAtb.Key.Atb = agentAtb.Value;
        }

        return this.nextTurn;
    }

    public TimelineAgent MoveToNextAgent()
    {
        this.refreshRemainingAgentsPerTurn = true;

        List<KeyValuePair<TimelineAgent, float>> allAgentsPriorities = this.GetAllAgentPrioritiesAfterCurrent();

        if(allAgentsPriorities.Count == 0) return null;

        this.currentAgentPriority = allAgentsPriorities[0];
        return this.currentAgentPriority.Key;
    }

    public Dictionary<int, List<TimelineAgent>> GetRemainingAgentsPerTurn()
    {
        Dictionary<int, List<TimelineAgent>> agentsPerTurn = new Dictionary<int, List<TimelineAgent>>();
        List<TimelineAgent> result = this.GetAllAgentPrioritiesAfterCurrent().Select((KeyValuePair<TimelineAgent, float> agentPriority) => {
                return agentPriority.Key;
            }).ToList();

        agentsPerTurn[this.turnNb] = result;

        if(this.nextTurn == null)
        {
            return agentsPerTurn;
        }

        foreach(KeyValuePair<int, List<TimelineAgent>> kvp in this.nextTurn.GetRemainingAgentsPerTurn())
        {
            agentsPerTurn[kvp.Key] = kvp.Value;
        }

        return agentsPerTurn;
    }

    private List<KeyValuePair<TimelineAgent, float>> GetAllAgentPrioritiesAfterCurrent()
    {
        List<KeyValuePair<TimelineAgent, float>> result = new List<KeyValuePair<TimelineAgent, float>>();
        KeyValuePair<TimelineAgent, float> upperAgentPriority = new KeyValuePair<TimelineAgent, float>(null, 1.1f);

        result = this.agentPriorities.GetViewBetween(this.currentAgentPriority, upperAgentPriority).ToList();

        return result;
    }

}