using UnityEngine;
using System.Collections.Generic;
using System;

public class SkillCompositeOld
{
/*     protected SkillComposite parent = null;
    
    protected List<SkillComposite> children = new List<SkillComposite>();
    public List<SkillComposite> Children => this.children;

    private SkillProcess skillProcess = null;

    private Func<SkillProcess, SkillProcess> initCallback;

    public SkillComposite(Func<SkillProcess, SkillProcess> initCallback)
    {
        this.initCallback = initCallback;
    }

    public bool Process()
    {
        return this.skillProcess.Process();
    }

    public void Init()
    {
        this.skillProcess = this.initCallback(this.parent?.skillProcess);
    }

    public SkillComposite Do(Func<SkillProcess, SkillProcess> initCallback)
    {
        this.AddChild(initCallback);
        return this;
    }

    public SkillComposite In(Func<SkillProcess, SkillProcess> initCallback)
    {
        SkillComposite child = this.AddChild(initCallback);
        return child;
    }

    public SkillComposite Out()
    {
        return this.parent;
    }

    private SkillComposite AddChild(Func<SkillProcess, SkillProcess> initCallback)
    {
        SkillComposite child = new SkillComposite(initCallback);
        child.parent = this;
        this.children.Add(child);

        return child;
    }  */
}