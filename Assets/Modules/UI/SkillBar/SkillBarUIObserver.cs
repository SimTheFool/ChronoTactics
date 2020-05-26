using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class SkillBarUIObserver : UIChildrenGenerator<SkillUIController>
{
    void Start()
    {
        ActorEvents.onPlayableStateEnabled += this.EnableUI;
        ActorEvents.onPlayableStateDisabled += this.DisableUI;
    }

    private void EnableUI(ActorFacade actor, Action<Skill> selectSkill)
    {
        this.RenderSkillButtons(actor.Skills.All, selectSkill);
    }

    private void DisableUI(ActorFacade actor)
    {
        this.RenderSkillButtons(actor.Skills.All, null, false);
    }

    private void RenderSkillButtons(List<Skill> skills, Action<Skill> onSkillClick = null, bool interactable = true)
    {
        this.Paint<Skill>(skills, (uI, skill) => {

            uI.SetSkillLabel(skill.Label);
            uI.ClearClickListeners();
            uI.Interactable = interactable;
            if(onSkillClick != null)
                uI.AddClickListener(() => onSkillClick(skill));
                
        });
    }
}
