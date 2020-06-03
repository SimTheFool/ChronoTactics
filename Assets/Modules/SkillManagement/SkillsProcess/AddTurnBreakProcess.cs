public class AddTurnBreakProcess :  SkillProcess
{
    private ITimelineAgent agent = null;

    public AddTurnBreakProcess(ITimelineAgent agent)
    {
        this.agent = agent;
    }

    public override bool Process()
    {
        DependencyLocator.getTimelineController().AddAgentTurnBreak(this.agent);
        return true;
    }
}