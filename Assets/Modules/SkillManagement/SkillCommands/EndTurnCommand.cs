public class EndTurnCommand : SkillCommand
{
    public override void Init(SkillInput input)
    {

    }

    public override bool Process(SkillInput input)
    {
        DependencyLocator.getTimelineController().EndPass();
        return true;
    }
}