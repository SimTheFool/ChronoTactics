using UnityEngine;

public class SkillEffect : ITimelineAgent
{
    private SkillQueueResolver skillQueueResolver = null;
    private TimelineController timelineController = null;

    private static int count = 0;

    private SkillInput skillInput = null;
    private Skill skill = null;
    private int effectDuration;

    public SkillEffect(SkillInput skillInput, Skill skill, int effectDuration)
    {
        this.skillQueueResolver = DependencyLocator.GetSkillQueueResolver();
        this.timelineController = DependencyLocator.getTimelineController();

        this.uniqId = this.timelineController.CurrentAgent.UniqId + SkillEffect.count;
        SkillEffect.count++;

        int atb = (int)Mathf.Floor(100 - this.timelineController.CurrentPriorityScore * this.Speed) % 100;
        atb = (atb + 100) % 100;
        this.Atb = atb;

        this.skillInput = skillInput;
        this.skill = skill;
        this.effectDuration = effectDuration;
    }

    // ITimelineAgent implementation.
    public TimelineAgentType agentType => TimelineAgentType.Effect;
    public string Name => this.skill.Label;

    private int uniqId;
    public int UniqId => this.uniqId;

    public int Atb {get; set;}
    public int Speed => 100;

    public void OnBeginPass()
    {
        this.skillQueueResolver.AddSkill(this.skillInput, this.skill);
        this.effectDuration--;
    }

    public void OnEndPass()
    {
        if(this.effectDuration == 0)
        {
            this.timelineController.RemoveAgent(this);
        }
    }
}