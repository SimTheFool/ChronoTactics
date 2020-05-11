using System;
using System.Linq;
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
    private bool isPaused = true;
    private Turn currentTurn = null;
    private ITimelineAgent currentAgent = null;

    public Dictionary<int, List<ITimelineAgent>> RemainingAgentsPerTurn => this.currentTurn?.RemainingAgentsPerTurn;
    public int RemainingAgentsCount => this.RemainingAgentsPerTurn.Aggregate(0, (acc, elem) => acc += elem.Value.Count);

    public void Init(List<ITimelineAgent> agents)
    {
        this.timer = 0;
        this.currentTurn = new Turn(new HashSet<ITimelineAgent>(agents));
        this.Play();
        this.OnTimelineChange();
    }

    public bool Process()
    {
        this.PreviewTurns();
        bool result =  this.PlayCurrentTurn();
        this.ProcessEventQueue();
        return result;
    }

    private void PreviewTurns()
    {
        while(this.RemainingAgentsCount < this.previewedAgentNb)
        {
            this.currentTurn.NewTurn();
            this.OnTimelineChange();
        }
    }

    private bool PlayCurrentTurn()
    {
        // We update the timer for current actor.
        if(!this.isPaused)
        {
            this.timer -= Time.deltaTime;
            this.OnTimerChange();
        }
        
        if(timer > 0) return false;

        // If time is up, we move to next agent for this turn.
        ITimelineAgent nextAgent = this.currentTurn.MoveToNextAgent();

        // If there is no next agent, we must change turn.
        if(nextAgent == null)
        {
            Turn nextTurn = this.currentTurn.MoveToNextTurn();

            // If there no next turn, we end combat.
            if(nextTurn == null) return true;

            // If there is, we get next agent of that turn.
            this.currentTurn = nextTurn;
            nextAgent = this.currentTurn.MoveToNextAgent();
        }

        this.currentAgent?.OnEndPass();
        this.currentAgent = nextAgent;
        this.currentAgent?.OnBeginPass();

        this.timer = this.secondsPerAgent;

        this.OnTimelineChange();
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
        this.OnTimelineChange();
    }

    public void RemoveAgent(ITimelineAgent agent)
    {
        this.currentTurn.RemoveAgent(agent);
        this.OnTimelineChange();
    }

    // Events
    private HashSet<Action> eventQueue = new HashSet<Action>();
    private void ProcessEventQueue()
    {
        if(this.eventQueue.Count <= 0)
            return;

        foreach(Action action in this.eventQueue)
        {
            action.Invoke();
        }

        this.eventQueue.Clear();
    }

    public event Action<Dictionary<int, List<ITimelineAgent>>> onTimelineChange;
    private void OnTimelineChange()
    {
        this.eventQueue.Add(() => {
            this.onTimelineChange?.Invoke(this.RemainingAgentsPerTurn);
        });
    }

    public event Action<float> onTimerChange;
    private void OnTimerChange()
    {
        this.eventQueue.Add(() => {
            this.onTimerChange?.Invoke(this.timer);
        });
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
