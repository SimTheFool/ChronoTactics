using UnityEngine;

public class TurnBreaksManagementState : TimelineState
{
    protected override float Timer
    {
        get => ((TurnBreaksManager)this.agentsCollectionManager).CurrentTimer;
        set => ((TurnBreaksManager)this.agentsCollectionManager).CurrentTimer = value;
    }

    public TurnBreaksManagementState(TimelineController timeline, TurnBreaksManager turnBreaksManager) : base(timeline, turnBreaksManager){}

    protected override void OnResetTimer(){}
    protected override void OnNextAgentNull() => this.timeline.SetPassesManagementState();
}