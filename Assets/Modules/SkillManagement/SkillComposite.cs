using UnityEngine;
using System.Collections.Generic;
using System;

public class SkillComposite 
{   
    private SkillComposite parent = null;

    private List<SkillComposite> children = new List<SkillComposite>();
    public List<SkillComposite> Children => this.children;

    private SkillProcess process = null;

    protected event Action<SkillComposite> onDoneProcessing;

    public SkillComposite(SkillProcess process = null)
    {
        this.process = process;
    }

    public bool Process()
    {
        return this.process.Process();
    }

    public void OnDoneProcessing()
    {
        this.onDoneProcessing?.Invoke(this);
    }

    public SkillComposite Do(Func<SkillProcess, SkillProcess> childInit)
    {
        this.AddChild(childInit);
        return this;
    }

    public SkillComposite DoSeveral(Func<SkillProcess, IEnumerable<SkillProcess>> childrenInit)
    {
        this.AddChildren(childrenInit);
        return this;
    }

    public SkillComposite In(Func<SkillProcess, SkillProcess> childInit)
    {
        SkillComposite child = this.AddChild(childInit);
        return child;
    }

    public SkillComposite InSeveral(Func<SkillProcess, IEnumerable<SkillProcess>> childrenInit)
    {
        SkillComposite child = this.AddChildren(childrenInit);
        return child;
    }

    public SkillComposite Out()
    {
        return this.parent;
    }

    private SkillComposite AddChild(Func<SkillProcess, SkillProcess> childInit)
    {
        int childIndex = this.CreateChild();

        this.onDoneProcessing += (thisComposite) => {
            SkillProcess process = childInit(thisComposite.process);
            thisComposite.children[childIndex].process = process;
        };

        return this.children[childIndex];
    }

    private SkillComposite AddChildren(Func<SkillProcess, IEnumerable<SkillProcess>> childrenInit)
    {
        int childIndex = this.CreateChild();

        this.onDoneProcessing += (thisComposite) => {
            IEnumerable<SkillProcess> processes = childrenInit(thisComposite.process);

            SkillComposite orginalChild = thisComposite.children[childIndex];
            thisComposite.children.RemoveAt(childIndex);

            foreach(SkillProcess process in processes)
            {
                int newChildIndex = thisComposite.CreateChild(orginalChild);
                thisComposite.children[newChildIndex].process = process;
            }
        };

        return this.children[childIndex];
    }

    private int CreateChild(SkillComposite original = null)
    {
        SkillComposite child = new SkillComposite();
        this.children.Add(child);

        child.parent = this;
        if(original != null)
        {
            child.onDoneProcessing += original.onDoneProcessing;
            foreach(SkillComposite grandChild in original.children)
            {
                child.CreateChild(grandChild);
            }
        }

        return this.children.Count - 1;
    }
}