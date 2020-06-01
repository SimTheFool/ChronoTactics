using UnityEngine;

public class PassesManagementState : TimelineState
{       
    private float timer;
    protected override float Timer
    {
        get => this.timer;
        set => this.timer = value;
    }

    public PassesManagementState(TimelineController timeline, PassesManager passesManager) : base(timeline, passesManager){}

    protected override void OnResetTimer() => this.Timer = this.timeline.SecondsPerPass;
    protected override void OnNextAgentNull() => Debug.Log("End Combat");
}