using UnityEngine;
using System.Collections.Generic;
using System;

public class SkillComposite 
{   
    private SkillComposite parent = null;

    private List<SkillComposite> children = new List<SkillComposite>();
    public List<SkillComposite> Children => this.children;

    private SkillProcess process = null;

    protected event Action<SkillProcess> onDoneProcessing;

    public SkillComposite(SkillProcess process = null)
    {
        this.process = process;
    }

    public bool Process()
    {
        //Debug.Log($"process {this.process}");
        return this.process.Process();
    }

    public void OnDoneProcessing()
    {
        this.onDoneProcessing?.Invoke(this.process);
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
        SkillComposite child = this.CreateChild();

        this.onDoneProcessing += (thisProcess) => {
            SkillProcess process = childInit(thisProcess);
            child.process = process;
        };

        return child;
    }

    private SkillComposite AddChildren(Func<SkillProcess, IEnumerable<SkillProcess>> childrenInit)
    {
        SkillComposite child = this.CreateChild();

        this.onDoneProcessing += (thisProcess) => {
            IEnumerable<SkillProcess> processes = childrenInit(thisProcess);
            foreach(SkillProcess process in processes)
            {
                SkillComposite newChild = this.CreateChild(child);
                newChild.process = process;
            }

            this.children.Remove(child);
        };

        return child;
    }

    private SkillComposite CreateChild(SkillComposite original = null)
    {
        SkillComposite child = new SkillComposite();
        this.children.Add(child);

        child.parent = this;
        if(original != null)
        {
            child.onDoneProcessing += original.onDoneProcessing;
            child.children = new List<SkillComposite>(original.children);
        }

        return child;
    }
}