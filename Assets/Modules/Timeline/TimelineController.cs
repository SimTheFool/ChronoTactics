using UnityEngine;
using System.Collections.Generic;

public class TimelineController : TimelineComponentsBridge
{
    new public TimelineEvents Events => base.Events;

    [SerializeField]
    private int secondsPerAgent = 20;

    private float timer = 0;
    private bool isPlaying = false;
    private ITimelineAgent currentAgent = null;

    public void Init(List<ITimelineAgent> agents)
    {
        this.timer = 0;
        this.Turns.Init(agents);
        this.Play();
    }

    public bool Process()
    {
        bool result = this.PlayCurrentTurn();
        this.ProcessEvents();
        return result;
    }

    private bool PlayCurrentTurn()
    {
        if(this.isPlaying)
        {
            this.timer -= Time.deltaTime;
            this.Events.OnTimerChange(this.timer);
        }
        
        if(timer > 0) return false;

        ITimelineAgent nextAgent = this.Turns.GetNextAgent();

        if(nextAgent == null)
            return true;

        this.currentAgent?.OnEndPass();
        this.currentAgent = nextAgent;
        this.currentAgent.OnBeginPass();
        
        this.timer = this.secondsPerAgent;

        return false;
    }

    private void ProcessEvents()
    {
        this.Events.Process();
    }

    public void Play()
    {
        this.isPlaying = true;
    }

    public void Pause()
    {
        this.isPlaying = false;
    }

    public void EndPass()
    {
        this.timer = -1;
    }

    public void AddOrUpdateAgent(ITimelineAgent agent)
    {
        this.Turns.AddOrUpdateAgent(agent);
    }

    public void RemoveAgent(ITimelineAgent agent)
    {
        this.Turns.RemoveAgent(agent);
    }
}