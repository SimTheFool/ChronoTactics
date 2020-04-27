public class ActivePlayableState : IBehaviourState
{
    private CombatControls combatControls = null;
    private Actor actor = null;

    public ActivePlayableState(Actor actor)
    {
        this.combatControls = DependencyLocator.GetCombatControls();
        this.actor = actor;
    }

    public void BehaviourUpdate()
    {

    }

    public void In()
    {
        this.combatControls.EnableCombatControls();
        this.combatControls.SetActions(this.actor.Actions);
        // @TODO manage UI callbacks
    }

    public void Out()
    {
        this.combatControls.DisableCombatControls();
    }
}