using System.Collections.Generic;
using System.Linq;
using System;

[System.Serializable]
public class Skill
{
    protected string skillLabel = "";
    public string Label => this.skillLabel;

    protected bool sync = true;

    // Invoked in order to build the composite tree.
    protected Func<SkillInput, SkillComposite> buildSkillCbk;

    private List<SkillComposite> composites;
    private List<bool> states;

    public Skill(Func<SkillInput, SkillComposite> buildSkillCbk, string skillLabel)
    {
        this.buildSkillCbk = buildSkillCbk;
        this.skillLabel = skillLabel;
    }

    // Return true if skill is done processing, else return false;
    public bool Process()
    {
        if(this.composites.Count == 0)
            return true;

        this.ProcessComposites();

        if(this.ShouldMoveToNextComposites())
            this.MoveToNextComposites();

        return false;
    }

    private void ProcessComposites()
    {
        for (int i = 0; i < this.composites.Count; i++)
        {
            if(this.states[i] == true)
                continue;

            this.states[i] = this.composites[i].Process();
        }
    }

    private bool ShouldMoveToNextComposites()
    {
        if(this.sync)
        {
            bool areAllCompositesDone = !this.states.Any(elem => elem == false);
            if(areAllCompositesDone)
                return true;
        }
        else
        {
            bool isOneCompositeDone = this.states.Any(elem => elem == true);
            if(isOneCompositeDone)
                return true;
        }

        return false;
    }

    private void MoveToNextComposites()
    {
        List<SkillComposite> nextComposites = new List<SkillComposite>();
        List<bool> nextStates = new List<bool>();

        for (int i = 0; i < this.composites.Count; i++)
        {
            SkillComposite composite = this.composites[i];
            bool isDoneProcessing = this.states[i];

            if(isDoneProcessing)
            {
                composite.OnDoneProcessing();

                foreach (SkillComposite child in composite.Children)
                {
                    nextComposites.Add(child);
                    nextStates.Add(false);
                }
                continue;
            }

            nextComposites.Add(composite);
            nextStates.Add(isDoneProcessing);
        }

        this.composites = nextComposites;
        this.states = nextStates;
    }

    public void Init(SkillInput input)
    {
        SkillComposite composite = this.buildSkillCbk(input);

        this.composites = new List<SkillComposite>(){composite};
        this.states = new List<bool>(){false};
    }
}