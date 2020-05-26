public class EndPassProcess :  SkillProcess
{
    public override bool Process()
    {
        DependencyLocator.getTimelineController().EndPass();
        return true;
    }
}