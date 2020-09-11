public class LoggingEffectSkill : OldSkill
{
    public LoggingEffectSkill() : base(null, "LoggingEffect")
    {
        this.buildSkillCbk = this.BuildSkill;
    }

    private SkillComposite BuildSkill(SkillInput input)
    {
        SkillComposite composite = new SkillComposite(new AddSkillEffectProcess(input, this.CreateLoggingEffect, "LoggingEffect", 3));
        return composite;
    }

    private SkillComposite CreateLoggingEffect(SkillInput skillInput)
    {
        SkillComposite composite = new SkillComposite(new DisplayWordProcess("logging effect" + skillInput.Caster.Name));
        return composite;
    }
}