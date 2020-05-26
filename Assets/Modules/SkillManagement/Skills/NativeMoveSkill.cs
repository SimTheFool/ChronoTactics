public class NativeMoveSkill : Skill
{
    public NativeMoveSkill()
    {
        this.skillLabel = "Move";
    }

    protected override SkillComposite BuildSkill(SkillInput input)
    {
        SkillComposite composite = new SkillComposite(new MoveProcess(input.Caster, input.TargetTile, new ManhattanTopology(), new WalkableFilter()));
        return composite;
    }
}