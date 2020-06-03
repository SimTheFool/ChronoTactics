using UnityEngine;
using System;

public class OnTakeDamageReactionProcess : SkillReactionProcess<ActorFacade>
{
    private int actorHealth;

    public OnTakeDamageReactionProcess(ActorFacade watchedActor, SkillInput skillInput, Skill skill) : base(watchedActor, skillInput, skill){}
    public OnTakeDamageReactionProcess(ActorFacade watchedActor, SkillInput skillInput, string skillLabel, Func<SkillInput, SkillComposite> buildSkillCbk) : base(watchedActor, skillInput, skillLabel, buildSkillCbk){}

    public override bool Process()
    {
        this.actorHealth = this.watchedElement.Stats.Health;
        this.watchedElement.Events.onStatsChange += this.OnTakeDamageListener;
        return true;
    }

    public void OnTakeDamageListener(ActorFacade actor)
    {
        int newHealth = actor.Stats.Health;

        if(actor.Stats.Health < this.actorHealth)
        {
            this.TriggerSkill();
            this.watchedElement.Events.onStatsChange -= this.OnTakeDamageListener;
            return;
        }

        this.actorHealth = newHealth;
    }
}