public class EndTurnCommand : SkillCommand
{
    public override void Init(SkillInput input)
    {

    }

    public override bool Process(SkillInput input)
    {
        DependencyLocator.getTimelineHandler().EndTurn();
        return true;
    }
}