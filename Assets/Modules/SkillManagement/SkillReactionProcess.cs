using UnityEngine;
using System;

public abstract class SkillReactionProcess<TWatchedElement> : SkillProcess where TWatchedElement : class
{
    protected TWatchedElement watchedElement = null;
    protected Skill skill = null;
    protected SkillInput skillInput = null;

    public SkillReactionProcess(TWatchedElement watchedElement, SkillInput skillInput, Skill skill)
    {
        this.Init(watchedElement, skillInput, skill);
    }

    public SkillReactionProcess(TWatchedElement watchedElement, SkillInput skillInput, string skillLabel, Func<SkillInput, SkillComposite> buildSkillCbk)
    {
        Skill skill = new Skill(buildSkillCbk, skillLabel);
        this.Init(watchedElement, skillInput, skill);
    }

    private void Init(TWatchedElement watchedElement, SkillInput skillInput, Skill skill)
    {
        this.watchedElement = watchedElement;
        this.skill = skill;
        this.skillInput = skillInput;
    }

    protected void TriggerSkill()
    {
        DependencyLocator.GetSkillQueueResolver().AddSkill(skillInput, skill);
    }

    public abstract override bool Process();
}