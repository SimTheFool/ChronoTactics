public class NativeMoveSkill : Skill
{
    public NativeMoveSkill() : base(null, "Move")
    {
        this.buildSkillCbk = this.BuildSkill;
    }

    private SkillComposite BuildSkill(SkillInput input)
    {
        SkillComposite composite = new SkillComposite(new MoveProcess(input.Caster, input.TargetTile, new ManhattanTopology(), new WalkableFilter()));
        return composite;
    }
}