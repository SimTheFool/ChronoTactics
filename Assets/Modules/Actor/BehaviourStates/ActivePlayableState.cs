using UnityEngine;

public class ActivePlayableState : IBehaviourState
{
    private Actor actor = null;
    private CombatControls combatControls = null;
    private SkillQueueResolver skillQueueResolver = null;
    private TilemapManager tilemapManager = null;

    private bool tempMappingFlag = false;

    public ActivePlayableState(Actor actor)
    {
        this.actor = actor;
        this.combatControls = DependencyLocator.GetCombatControls();
        this.skillQueueResolver = DependencyLocator.GetSkillQueueResolver();
        this.tilemapManager = DependencyLocator.getTilemapManager();
    }

    public void BehaviourUpdate()
    {
        if(!tempMappingFlag)
        {
            // Nothing to manage for now
        }
        else
        {
            if(Input.GetMouseButton(0))
            {
                this.ResolveSkill(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }

            if(Input.GetMouseButton(1))
            {
                this.CancelSkill();
                this.ChangeControlMapping(false);
            }
        }
    }

    public void In()
    {

        this.combatControls.Enable();
        this.combatControls.SetActor(this.actor);
        this.combatControls.SetClickListener(this.SkillUIClickListener);
    }

    public void Out()
    {
        this.combatControls.Disable();
    }

    private void SelectSkill(Skill skill)
    {
        this.actor.SelectedSkill = skill;
    }

    private void CancelSkill()
    {
        this.actor.SelectedSkill = null;
    }

    private void ResolveSkill(Vector3 pos)
    {
        if(this.actor.SelectedSkill == null) return;

        TileFacade targetTile = this.tilemapManager.GetTileFromWorldPos(pos);
        if(targetTile == null) return;

        Actor caster = this.actor;
        Actor targetActor = (Actor)targetTile.Agent;
        SkillInput input = new SkillInput(caster, targetTile, targetActor);

        this.skillQueueResolver.AddSkill(input, this.actor.SelectedSkill);

        this.CancelSkill();
    }

    private void ChangeControlMapping(bool value)
    {
        this.tempMappingFlag = value;
    }

    private void SkillUIClickListener(Skill skill)
    {
        if(skill != this.actor.SelectedSkill)
        {
            this.SelectSkill(skill);
            this.ChangeControlMapping(true);
            return;
        }

        this.CancelSkill();
        this.ChangeControlMapping(false);
    }
}