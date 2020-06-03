using UnityEngine;

public class GiveTurnBreakSkill : Skill
{
    public GiveTurnBreakSkill() : base(null, "Give Turn Break")
    {
        this.buildSkillCbk = this.BuildSkill;
    }

    private SkillComposite BuildSkill(SkillInput input)
    {
        SkillComposite composite = new SkillComposite(new AddTurnBreakProcess(input.TargetActor));
        return composite;
    }
}