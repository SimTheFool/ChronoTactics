using UnityEngine;
using System.Collections.Generic;
using System;

public class TimelineEvents : TimelineComponentsBridge
{
    private HashSet<Action> eventQueue = new HashSet<Action>();
    public void Process()
    {
        if(this.eventQueue.Count <= 0)
            return;

        foreach(Action action in this.eventQueue)
        {
            action.Invoke();
        }

        this.eventQueue.Clear();
    }

    public event Action<Dictionary<int, List<ITimelineAgent>>, ITimelineAgent> onTimelineChange;
    public void OnTimelineChange(Dictionary<int, List<ITimelineAgent>> remainingAgentsPerTurn, ITimelineAgent currentAgent)
    {
        this.eventQueue.Add(() => {
            this.onTimelineChange?.Invoke(remainingAgentsPerTurn, currentAgent);
        });
    }

    /* public event Action<ITimelineAgent> onAgentChange;
    public void OnAgentChange(ITimelineAgent newAgent)
    {
        this.eventQueue.Add(() => {
            this.onAgentChange?.Invoke(newAgent);
        });
    } */

    public event Action<float> onTimerChange;
    public void OnTimerChange(float timer)
    {
        this.eventQueue.Add(() => {
            this.onTimerChange?.Invoke(timer);
        });
    }
}