public class NativeEndPassSkill : Skill
{
    public NativeEndPassSkill()
    {
        this.skillLabel = "End";
    }

    protected override SkillComposite BuildSkill(SkillInput input)
    {
        SkillComposite composite = new SkillComposite(new EndPassProcess());
        return composite;
    }
}