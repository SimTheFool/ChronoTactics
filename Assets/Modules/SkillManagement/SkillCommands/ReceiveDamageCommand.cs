public class ReceiveDamageCommand : SkillCommand
{
    public override void Init(SkillInput input)
    {

    }

    public override bool Process(SkillInput input)
    {
        input.Caster.Stats.Health -= 10;
        return true;
    }
}