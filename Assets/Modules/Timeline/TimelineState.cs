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
        this.MoveToNextAgent();
        this.CurrentAgent?.OnBeginPass();

        if(this.CurrentAgent == null)
            this.OnNextAgentNull();
    }

    public void EndTurn() => this.Timer = -1;

    public void Enter()
    {
        if(this.CurrentAgent == null)
            this.MoveToNextAgent();

        if(this.CurrentAgent == null)
            this.OnNextAgentNull();

         this.CurrentAgent?.OnBeginPass();
    }

    public void Exit() => this.CurrentAgent?.OnEndPass();

    protected abstract void OnResetTimer();
    protected abstract void OnNextAgentNull();

    private void MoveToNextAgent()
    {
        this.OnResetTimer();
        this.agentsCollectionManager.MoveToNextAgent();
    }

}