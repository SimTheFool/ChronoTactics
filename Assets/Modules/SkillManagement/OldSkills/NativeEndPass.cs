public class NativeEndPassSkill : OldSkill
{
    public NativeEndPassSkill() : base(null, "End")
    {
        this.buildSkillCbk = this.BuildSkill;
    }

    private SkillComposite BuildSkill(SkillInput input)
    {
        SkillComposite composite = new SkillComposite(new EndPassProcess());
        return composite;
    }
}