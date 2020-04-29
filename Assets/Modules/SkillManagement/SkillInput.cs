public class SkillInput
{
    private Actor caster;
    private TileFacade targetTile;
    private Actor targetActor;

    public Actor Caster
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

    public Actor TargetActor
    {
        get
        {
            return this.targetActor;
        }
    }

    public SkillInput(Actor caster, TileFacade targetTile, Actor targetActor = null)
    {
        this.caster = caster;
        this.targetTile = targetTile;
        this.targetActor = targetActor;
    }
}