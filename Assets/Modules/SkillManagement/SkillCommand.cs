public class SkillCommand
{
    public virtual void Init(SkillInput input)
    {

    }

    public virtual bool Process(SkillInput input)
    {
        return false;
    }
}