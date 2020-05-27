public class LoggingOnDamageSkill : Skill
{
    public LoggingOnDamageSkill() : base(null, "Logging on damage")
    {
        this.buildSkillCbk = this.BuildSkill;
    }

    private SkillComposite BuildSkill(SkillInput input)
    {
        SkillComposite composite = new SkillComposite(new OnTakeDamageReactionProcess(input.TargetActor, null, new LoggingSkill()));
        return composite;
    }
}