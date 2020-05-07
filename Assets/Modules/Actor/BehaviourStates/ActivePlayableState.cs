using UnityEngine;
using UnityEngine.InputSystem;

public class ActivePlayableState : IBehaviourState
{
    private Actor actor = null;
    private ActorSkills skills = null;

    private CombatControlsUI combatControlsUI = null;
    private CombatActionsMapping combatActionsMapping = null;

    private SkillQueueResolver skillQueueResolver = null;
    private TilemapManager tilemapManager = null;

    public ActivePlayableState(Actor actor)
    {
        this.actor = actor;
        this.skills = actor.Skills;

        this.combatControlsUI = DependencyLocator.GetCombatControls();
        this.combatActionsMapping = DependencyLocator.GetActionsMapper<CombatActionsMapping>();
        this.skillQueueResolver = DependencyLocator.GetSkillQueueResolver();
        this.tilemapManager = DependencyLocator.getTilemapManager();
    }

    public void In()
    {
        this.combatControlsUI.Enable();
        this.combatControlsUI.SetActor(this.actor);
        this.combatControlsUI.SetClickListener(this.SkillUIClickListener);

        this.SwitchToSkillNotSelectedMapping();

        this.combatActionsMapping.SkillSelectedMapping.ResolveSkill.performed += this.ResolveSkillListener;
        this.combatActionsMapping.SkillSelectedMapping.CancelSkill.performed += this.CancelSkillListener;
    }

    public void Out()
    {
        this.combatControlsUI.Disable();
        this.combatActionsMapping.SkillSelectedMapping.ResolveSkill.performed -= this.ResolveSkillListener;
        this.combatActionsMapping.SkillSelectedMapping.CancelSkill.performed -= this.CancelSkillListener;
    }

    private void SelectSkill(Skill skill)
    {
        this.skills.Selected = skill;
    }

    private void CancelSkill()
    {
        this.skills.Selected = null;
    }

    private void ResolveSkill(Vector3 pos)
    {
        if(this.skills.Selected == null) return;

        TileFacade targetTile = this.tilemapManager.GetTileFromWorldPos(pos);
        if(targetTile == null) return;

        Actor caster = this.actor;
        Actor targetActor = (Actor)targetTile.Agent;
        SkillInput input = new SkillInput(caster, targetTile, targetActor);

        this.skillQueueResolver.AddSkill(input, this.skills.Selected);

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
        if(skill != this.skills.Selected)
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