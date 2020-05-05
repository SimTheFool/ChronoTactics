using System.Collections.Generic;
using UnityEngine;

public class TimelineController : MonoBehaviour
{
    [SerializeField]
    private int previewedAgentNb = 0;
    [SerializeField]
    private int secondsPerAgent = 20;

    private float timer = 0;
    public float Timer => this.timer;
    private bool isPaused = false;
    private Turn currentTurn = null;
    private ITimelineAgent currentAgent = null;

    public Dictionary<int, List<ITimelineAgent>> RemainingAgentsPerTurn
    {
        get
        {
            return this.currentTurn?.RemainingAgentsPerTurn;
        }
    }


    public void Init(List<ITimelineAgent> agents)
    {
        this.timer = 0;
        this.currentTurn = new Turn(new HashSet<ITimelineAgent>(agents));
        this.Play();
    }

    public bool Process()
    {
        this.PreviewTurns();
        return this.PlayCurrentTurn();
    }

    private void PreviewTurns()
    {
        while(this.currentTurn.Count < this.previewedAgentNb)
        {
            this.currentTurn.NewTurn();
        }
    }

    private bool PlayCurrentTurn()
    {
        // We update the timer for current actor.
        if(!this.isPaused) this.timer -= Time.deltaTime;
        if(timer > 0) return false;

        // If time is up, we move to next agent for this turn.
        ITimelineAgent nextAgent = this.currentTurn.MoveToNextAgent();

        // If there is no next agent, we must change turn.
        if(nextAgent == null)
        {
            Turn nextTurn = this.currentTurn.MoveToNextTurn();

            // If there no next turn, we end combat.
            if(nextTurn == null)
            {
                return true;
            }

            // If there is, we get next agent of that turn.
            this.currentTurn = nextTurn;
            nextAgent = this.currentTurn.MoveToNextAgent();
            return false;
        }

        this.currentAgent?.OnEndPass();
        this.currentAgent = nextAgent;
        this.currentAgent.OnBeginPass();

        this.timer = this.secondsPerAgent;

        return false;
    }

    private void Pause()
    {
        this.isPaused = true;
    }

    private void Play()
    {
        this.isPaused = false;
    }
    
    public void EndPass()
    {
        this.timer = -1;
    }

    public void AddOrUpdateAgent(ITimelineAgent agent)
    {
        this.currentTurn.AddOrUpdateAgent(agent);
    }

    public void RemoveAgent(ITimelineAgent agent)
    {
        this.currentTurn.RemoveAgent(agent);
    }

    /* private void OnGUI()
    {
        int dim = 20;

        GUI.Label(new Rect(10, 10, 100, 20), $"{this.timer}");

        if(this.turn == null) return;

        Dictionary<int, List<TimelineAgent>> agentsPerTurn = this.turn.RemainingAgentsPerTurn;

        foreach(int turnNb in agentsPerTurn.Keys)
        {
            int k = 0;
            foreach(TimelineAgent agent in agentsPerTurn[turnNb])
            {
                k++;
                GUI.Label(new Rect(100 + k*dim, 10 + turnNb*dim*3, dim, dim), $"{turnNb}");
                GUI.Label(new Rect(100 + k*dim, 10 + turnNb*dim*3 + dim, dim, dim), $"{agent.Name.Substring(0,2)}");
                GUI.Label(new Rect(100 + k*dim, 10 + turnNb*dim*3 + 2*dim, dim, dim), $"{agent.Speed}");
            }
        }
    } */
}
