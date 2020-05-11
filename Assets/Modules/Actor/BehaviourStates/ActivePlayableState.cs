using UnityEngine;
using UnityEngine.InputSystem;

public class ActivePlayableState : IBehaviourState
{
    private ActorFacade actor = null;

    private CombatActionsMapping combatActionsMapping = null;

    private SkillQueueResolver skillQueueResolver = null;
    private TilemapManager tilemapManager = null;

    public ActivePlayableState(ActorFacade actor)
    {
        this.actor = actor;

        this.combatActionsMapping = DependencyLocator.GetActionsMapper<CombatActionsMapping>();
        this.skillQueueResolver = DependencyLocator.GetSkillQueueResolver();
        this.tilemapManager = DependencyLocator.getTilemapManager();
    }

    public void In()
    {
        ActorEvents.OnPlayableStateEnabled(this.actor, this.SkillUIClickListener);

        this.SwitchToSkillNotSelectedMapping();
        this.combatActionsMapping.SkillSelectedMapping.ResolveSkill.performed += this.ResolveSkillListener;
        this.combatActionsMapping.SkillSelectedMapping.CancelSkill.performed += this.CancelSkillListener;
    }

    public void Out()
    {
        ActorEvents.OnPlayableStateDisabled(this.actor);

        this.combatActionsMapping.SkillSelectedMapping.ResolveSkill.performed -= this.ResolveSkillListener;
        this.combatActionsMapping.SkillSelectedMapping.CancelSkill.performed -= this.CancelSkillListener;
    }

    private void SelectSkill(Skill skill)
    {
        this.actor.Skills.Selected = skill;
    }

    private void CancelSkill()
    {
        this.actor.Skills.Selected = null;
    }

    private void ResolveSkill(Vector3 pos)
    {
        if(this.actor.Skills.Selected == null) return;

        TileFacade targetTile = this.tilemapManager.GetTileFromWorldPos(pos);
        if(targetTile == null) return;

        ActorFacade caster = this.actor;
        ActorFacade targetActor = (ActorFacade)targetTile.Agent;
        SkillInput input = new SkillInput(caster, targetTile, targetActor);

        this.skillQueueResolver.AddSkill(input, this.actor.Skills.Selected);

        this.CancelSkill();
    }

    private void SwitchToSkillSelectedMapping()
    {
        this.combatActionsMapping.SkillSelectedMapping.Enable();
        this.combatActionsMapping.SkillNotSelectedMapping.Disable();
    }

    private void SwitchToSkillNotSelectedMapping()
    {
        this.combatActionsMapping.SkillSelectedMapping.Disable();
        this.combatActionsMapping.SkillNotSelectedMapping.Enable();
    }

    private void SkillUIClickListener(Skill skill)
    {
        if(skill != this.actor.Skills.Selected)
        {
            this.SelectSkill(skill);
            this.SwitchToSkillSelectedMapping();
            return;
        }

        this.CancelSkill();
        this.SwitchToSkillNotSelectedMapping();
    }

    private void ResolveSkillListener(InputAction.CallbackContext ctx)
    {
        Vector3 pos = Input.mousePosition;
        pos = MainCamera.Instance.ScreenToWorldPoint((Vector3)pos);
        this.ResolveSkill(pos);
    }

    private void CancelSkillListener(InputAction.CallbackContext ctx)
    {
        this.CancelSkill();
        this.SwitchToSkillNotSelectedMapping();
    }
}