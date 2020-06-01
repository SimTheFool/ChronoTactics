using UnityEngine;
using System.Collections.Generic;
using System;

public class TimelineEvents : TimelineComponentsBridge
{
    private HashSet<Action> eventQueue = new HashSet<Action>();

    public event Action<Dictionary<int, List<ITimelineAgent>>, ITimelineAgent> onPassesChange;
    public void OnPassesChange(Dictionary<int, List<ITimelineAgent>> remainingAgentsPerPass, ITimelineAgent currentAgent)
    {
        this.eventQueue.Add(() => {
            this.onPassesChange?.Invoke(remainingAgentsPerPass, currentAgent);
        });
    }

    public event Action<IEnumerable<ITimelineAgent>> onTurnBreaksChange;
    public void OnTurnBreaksChange(IEnumerable<ITimelineAgent> remainingTurnBreaks)
    {
        this.eventQueue.Add(() => {
            this.onTurnBreaksChange?.Invoke(remainingTurnBreaks);
        });
    }

    public event Action<float> onTimerChange;
    public void OnTimerChange(float timer)
    {
        this.eventQueue.Add(() => {
            this.onTimerChange?.Invoke(timer);
        });
    }

    public void Update()
    {
        if(this.eventQueue.Count <= 0)
            return;

        foreach(Action action in this.eventQueue)
        {
            action.Invoke();
        }

        this.eventQueue.Clear();
    }
}