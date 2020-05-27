using UnityEngine;
using System;

public class AddSkillEffectProcess : SkillProcess
{
    private SkillInput skillInput = null;
    private Skill skill = null;
    private int effectDuration;

    public AddSkillEffectProcess(SkillInput skillInput, Skill skill, int effectDuration)
    {
        this.Init(skillInput, skill, effectDuration);
    }

    public AddSkillEffectProcess(SkillInput skillInput, Func<SkillInput, SkillComposite> buildSkillCbk, string skillLabel, int effectDuration)
    {
        Func<SkillInput, SkillComposite> autoEndedBuildSkillCbk = (input) => {
            return buildSkillCbk(input).Do((parentProcess) => new EndPassProcess());
        };
        Skill skill = new Skill(autoEndedBuildSkillCbk, skillLabel);
        
        this.Init(skillInput, skill, effectDuration);
    }

    private void Init( SkillInput skillInput, Skill skill, int effectDuration)
    {
        this.skillInput = skillInput;
        this.skill = skill;
        this.effectDuration = effectDuration;
    }

    public override bool Process()
    {
        DependencyLocator.getTimelineController().AddOrUpdateAgent(new SkillEffect(this.skillInput, this.skill, this.effectDuration));
        return true;
    }
}