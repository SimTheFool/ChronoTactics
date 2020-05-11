public class SkillInput
{
    private ActorFacade caster;
    private TileFacade targetTile;
    private ActorFacade targetActor;

    public ActorFacade Caster
    {
        get
        {
            return this.caster;
        }
    }

    public TileFacade TargetTile
    {
        get
        {
            return this.targetTile;
        }
    }

    public ActorFacade TargetActor
    {
        get
        {
            return this.targetActor;
        }
    }

    public SkillInput(ActorFacade caster, TileFacade targetTile, ActorFacade targetActor = null)
    {
        this.caster = caster;
        this.targetTile = targetTile;
        this.targetActor = targetActor;
    }
}