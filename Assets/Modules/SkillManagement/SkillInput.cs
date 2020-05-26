public class SkillInput
{
    private ActorFacade caster;
    private TileFacade targetTile;
    private ActorFacade targetActor;

    public ActorFacade Caster => this.caster;
    public TileFacade TargetTile => this.targetTile;
    public ActorFacade TargetActor => this.targetActor;

    public SkillInput(ActorFacade caster, TileFacade targetTile, ActorFacade targetActor = null)
    {
        this.caster = caster;
        this.targetTile = targetTile;
        this.targetActor = targetActor;
    }
}