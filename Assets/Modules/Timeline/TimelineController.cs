using UnityEngine;
using System.Collections.Generic;

public class TimelineController : TimelineComponentsBridge
{
    new public TimelineEvents Events => base.Events;

    [SerializeField]
    private int secondsPerPass = 20;
    public int SecondsPerPass => this.secondsPerPass;

    public ITimelineAgent CurrentPassAgent => this.Passes.CurrentAgent;
    public float CurrentPassPriorityScore => this.Passes.CurrentPriorityScore;

    private TimelineState state = null;
    private PassesManagementState passesManagementState = null;
    private TurnBreaksManagementState turnBreaksManagementState = null;

    public void Init(List<ITimelineAgent> agents)
    {
        this.Passes.Init(agents);

        this.passesManagementState = new PassesManagementState(this, this.Passes);
        this.turnBreaksManagementState = new TurnBreaksManagementState(this, this.TurnBreaks);
        this.SetPassesManagementState();
    }

    public void Update()
    {
        this.state.Process();
    }

    private void SetState(TimelineState state)
    {
        this.state?.Exit();
        this.state = state;
        this.state?.Enter();
    }

    public void SetTurnBreaksManagementState()
    {
        this.SetState(this.turnBreaksManagementState);
    }

    public void SetPassesManagementState()
    {
        this.SetState(this.passesManagementState);
    }

    public void EndPass()
    {
        this.state.EndTurn();
    }

    public void AddAgentInPasses(ITimelineAgent agent)
    {
        this.Passes.Add(agent);
    }

    public void AddAgentTurnBreak(ITimelineAgent agent)
    {
        this.TurnBreaks.Add(agent);
        this.SetTurnBreaksManagementState();
    }

    public void RemoveAgent(ITimelineAgent agent)
    {
        this.Passes.Remove(agent);
        this.TurnBreaks.Remove(agent);
    }
}