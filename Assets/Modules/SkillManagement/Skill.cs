using System.Collections.Generic;
using System.Linq;

public abstract class Skill
{
    protected string skillName = "";
    public string Name => this.skillName;

    protected bool sync = true;

    protected Dictionary<SkillComposite, bool> statePerComposites;

    // Return true if skill is done processing, else return false;
    public bool Process()
    {
        if(this.statePerComposites.Count == 0)
            return true;

        this.ProcessComposites();
        if(this.ShouldMoveToNextComposites())
            this.MoveToNextComposites();

        return false;
    }

    private void ProcessComposites()
    {
        List<SkillComposite> composites = new List<SkillComposite>(this.statePerComposites.Keys);

        foreach (SkillComposite composite in composites)
        {
            if(this.statePerComposites[composite] == true)
                continue;

            this.statePerComposites[composite] = composite.Process();
        }
    }

    private bool ShouldMoveToNextComposites()
    {
        if(this.sync)
        {
            bool areAllCompositesDone = !this.statePerComposites.Any(elem => elem.Value == false);
            if(areAllCompositesDone)
                return true;
        }
        else
        {
            bool isOneCompositeDone = this.statePerComposites.Any(elem => elem.Value == true);
            if(isOneCompositeDone)
                return true;
        }

        return false;
    }

    private void MoveToNextComposites()
    {
        Dictionary<SkillComposite, bool> nextStatePerComposites = new Dictionary<SkillComposite, bool>();
        foreach (KeyValuePair<SkillComposite, bool> kvp in this.statePerComposites)
        {
            SkillComposite composite = kvp.Key;
            bool isDoneProcessing = kvp.Value;

            if(isDoneProcessing)
            {
                foreach (SkillComposite child in composite.Children)
                {
                    child.Init();
                    nextStatePerComposites[child] = false;
                }
                continue;
            }

            nextStatePerComposites[composite] = isDoneProcessing;
        }

        this.statePerComposites = nextStatePerComposites;
    }

    // Invoked in order to build the composite tree.
    public abstract void BuildSkill(SkillInput input);
}