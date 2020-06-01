using UnityEngine;

public abstract class TimelineState
{
    protected TimelineController timeline = null;
    protected ITimelineAgentsCollectionManager agentsCollectionManager = null;

    private ITimelineAgent CurrentAgent => this.agentsCollectionManager.CurrentAgent;
    protected abstract float Timer {get; set;}

    public TimelineState(TimelineController timeline, ITimelineAgentsCollectionManager agentsCollectionManager)
    {
        this.timeline = timeline;
        this.agentsCollectionManager = agentsCollectionManager;
    }

    public virtual void Process()
    {
        this.Timer -= Time.deltaTime;
        this.timeline.Events.OnTimerChange(this.Timer);

        if(this.Timer > 0)
            return;

        this.CurrentAgent?.OnEndPass();

        this.OnResetTimer();
        this.agentsCollectionManager.MoveToNextAgent();

        this.CurrentAgent?.OnBeginPass();

        if(this.CurrentAgent == null)
            this.OnNextAgentNull();
    }

    protected abstract void OnResetTimer();
    protected abstract void OnNextAgentNull();

    public void EndTurn() => this.Timer = -1;

    public void Enter()
    {
        if(this.CurrentAgent == null)
            this.agentsCollectionManager.MoveToNextAgent();

        if(this.CurrentAgent == null)
            this.OnNextAgentNull();

         this.CurrentAgent?.OnBeginPass();
    }

    public void Exit() => this.CurrentAgent?.OnEndPass();
}