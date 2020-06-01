using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pass
{
    private Dictionary<ITimelineAgent, int> inputAgentsAtbs;
    private Dictionary<ITimelineAgent, int> outputAgentsAtbs;

    private SortedSet<(ITimelineAgent agent, float priorityScore)> agentsPriorityScores;
    private class CompareAgentPriorityScores : IComparer<(ITimelineAgent agent, float priorityScore)>
    {
        public int Compare((ITimelineAgent agent, float priorityScore) p1, (ITimelineAgent agent, float priorityScore) p2)
        {
            if(p1.agent == p2.agent && p1.priorityScore == p2.priorityScore) return 0;            

            int result = p1.priorityScore.CompareTo(p2.priorityScore);

            result = (result == 0) ? p2.agent.Speed.CompareTo(p1.agent.Speed) : result;
            result = (result == 0) ? p1.agent.UniqId.groupId.CompareTo(p2.agent.UniqId.groupId) : result;
            result = (result == 0) ? p1.agent.UniqId.selfId.CompareTo(p2.agent.UniqId.selfId) : result;
            return result;
        }
    }

    private (ITimelineAgent agent, float priorityScore) lowerAgentPriorityScore = (null, -1);
    private (ITimelineAgent agent, float priorityScore) upperAgentPriorityScore = (null, 2);

    private int passNb = 0;
    private Pass nextPass = null;

    public Pass (Dictionary<ITimelineAgent, int> agentsAtbs, int passNb)
    {
        this.inputAgentsAtbs = new Dictionary<ITimelineAgent, int>(agentsAtbs);
        this.outputAgentsAtbs = new Dictionary<ITimelineAgent, int>();
        this.agentsPriorityScores = new SortedSet<(ITimelineAgent agent, float priorityScore)>(new CompareAgentPriorityScores());
        this.passNb = passNb;

        foreach(ITimelineAgent agent in this.inputAgentsAtbs.Keys)
        {
            this.EvaluatePriorityScores(agent);
        }
    }

    private void EvaluatePriorityScores(ITimelineAgent agent)
    {
        int currentAtb = this.inputAgentsAtbs[agent];
        int finalAtb = currentAtb + agent.Speed;
        int canPlayNb = finalAtb / 100;

        for(int i = 1; i <= canPlayNb; i++)
        {
            float priority = ((i * 100) - currentAtb) / (float)agent.Speed;
            this.agentsPriorityScores.Add((agent, priority));
        }
        
        this.outputAgentsAtbs[agent] = finalAtb % 100;
    }

    private void RemovePriorityScores(ITimelineAgent agent)
    {
        this.agentsPriorityScores.RemoveWhere(agentPriority => {
            return agentPriority.agent == agent;
        });
    }

    public void AddOrUpdateAgent(ITimelineAgent agent, int atb)
    {
        this.inputAgentsAtbs[agent] = atb;

        this.RemovePriorityScores(agent);
        this.EvaluatePriorityScores(agent);
        
        if(this.nextPass == null)
            return;

        this.nextPass.AddOrUpdateAgent(agent, this.outputAgentsAtbs[agent]);
    }

    public void RemoveAgent(ITimelineAgent agent)
    {
        this.inputAgentsAtbs.Remove(agent);
        this.outputAgentsAtbs.Remove(agent);
        this.RemovePriorityScores(agent);
        
        if(this.nextPass == null)
            return;

        this.nextPass.RemoveAgent(agent);
    }

    public void NewPass()
    {
        if(this.outputAgentsAtbs.Count == 0)
            return;

        if(this.nextPass != null)
        {
            this.nextPass.NewPass();
            return;
        }
            
        this.nextPass = new Pass(this.outputAgentsAtbs, this.passNb + 1);
    }

    public Pass GetNextPass()
    {
        foreach(KeyValuePair<ITimelineAgent, int> agentAtb in this.outputAgentsAtbs)
        {
            agentAtb.Key.Atb = agentAtb.Value;
        }

        return this.nextPass;
    }

    public (ITimelineAgent agent, float priorityScore) GetAgentPriorityScoreNextTo(ITimelineAgent agent, float priorityScore)
    {
        List<(ITimelineAgent agent, float priorityScore)> agentsPriorityScores = this.GetAllAgentsPriorityScoreNextTo(agent, priorityScore);

        if(agentsPriorityScores.Count == 0)
            return (null, -1);

        (ITimelineAgent nextAgent, float nextPriorityScore) = agentsPriorityScores[0];
        return (nextAgent, nextPriorityScore);
    }

    private List<(ITimelineAgent agent, float priorityScore)> GetAllAgentsPriorityScoreNextTo(ITimelineAgent agent, float priorityScore)
    {
        List<(ITimelineAgent agent, float priorityScore)> result = new List<(ITimelineAgent agent, float priorityScore)>();

        (ITimelineAgent agent, float priorityScore) lower = (agent == null) ? this.lowerAgentPriorityScore : (agent, priorityScore);

        result = this.agentsPriorityScores.GetViewBetween(lower, this.upperAgentPriorityScore).ToList();
        result.Remove((agent, priorityScore));

        return result;
    }

    public Dictionary<int, List<ITimelineAgent>> GetActorsPerPassNextTo(ITimelineAgent agent, float priorityScore)
    {
        Dictionary<int, List<ITimelineAgent>> result = new Dictionary<int, List<ITimelineAgent>>();

        List<ITimelineAgent> actors = this.GetAllAgentsPriorityScoreNextTo(agent, priorityScore)
            .Select(pair => pair.agent)
            .Where(agentActor => agentActor.agentType == TimelineAgentType.Actor)
            .ToList();
            
        result[this.passNb] = actors;

        if(this.nextPass == null)
            return result;

        foreach(KeyValuePair<int, List<ITimelineAgent>> kvp in this.nextPass.GetActorsPerPassNextTo(null, priorityScore))
        {
            result[kvp.Key] = kvp.Value;
        }

        return result;
    }
}