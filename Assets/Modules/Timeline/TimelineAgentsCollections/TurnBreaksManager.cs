using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TurnBreaksManager : TimelineComponentsBridge, ITimelineAgentsCollectionManager
{
    private class TimerAgentPair
    {
        public float timer;
        public ITimelineAgent agent = null;

        public TimerAgentPair(float timer, ITimelineAgent agent)
        {
            this.timer = timer;
            this.agent = agent;
        }
    }

    [SerializeField]
    private int secondsPerTurnBreak = 10;

    private LinkedList<TimerAgentPair> turnBreaks = new LinkedList<TimerAgentPair>();

    public ITimelineAgent CurrentAgent => this.turnBreaks.First().agent;
    public float CurrentTimer
    {
        get => this.turnBreaks.First().timer;
        set => this.turnBreaks.First().timer = value;
    }

    public void MoveToNextAgent()
    {
        this.turnBreaks.RemoveFirst();
        this.OnTurnBreaksChange();
    }

    public void Add(ITimelineAgent agent)
    {
        this.turnBreaks.AddFirst(new TimerAgentPair(this.secondsPerTurnBreak, agent));
        this.OnTurnBreaksChange();
    }

    public void Remove(ITimelineAgent agent)
    {
        IEnumerable<TimerAgentPair> turnBreaksEnumerable = this.turnBreaks.Where(timerAgentPair => timerAgentPair.agent != agent);
        this.turnBreaks = new LinkedList<TimerAgentPair>(turnBreaksEnumerable);
        this.OnTurnBreaksChange();
    }

    private void OnTurnBreaksChange()
    {
        this.Events.OnTurnBreaksChange(this.turnBreaks.Select(timerAgentPair => timerAgentPair.agent));
    }
}