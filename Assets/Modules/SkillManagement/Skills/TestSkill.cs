using UnityEngine;

public class TestSkill : Skill
{
    public TestSkill()
    {
        this.skillName = "test skill";
    }

    protected override SkillComposite BuildSkill(SkillInput input)
    {
        return new SkillComposite(new DisplayWordProcess("bo belinj"));
    }
}