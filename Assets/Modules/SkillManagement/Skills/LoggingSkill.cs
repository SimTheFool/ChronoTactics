using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class LoggingSkill : Skill
{
    public LoggingSkill()
    {
        this.skillName = "logging skill";
    }

    protected override SkillComposite BuildSkill(SkillInput input)
    {

        SkillComposite composite = new SkillComposite(new DoubleWordProcess("foo"));       

        Func<SkillProcess, DisplayWordProcess> onDoubleWord_DisplayWord = (parentProcess) => {
            DoubleWordProcess comp = (DoubleWordProcess)parentProcess;
            return new DisplayWordProcess(comp.OutputWord);
        };

        Func<SkillProcess, DoubleWordProcess> onDoubleWord_DoubleWord = (parentProcess) => {
            DoubleWordProcess comp = (DoubleWordProcess)parentProcess;
            return new DoubleWordProcess(comp.OutputWord);
        };

        composite
        /* .In(onDoubleWord_DoubleWord)
            .Do(onDoubleWord_DisplayWord)
        .Out()
        .Do(onDoubleWord_DisplayWord) */
        .In(parentProcess => new SplitStringProcess("abc def.ghi.jkl mno", '.'))
            .InSeveral(parentProcess => {
                SplitStringProcess parent = (SplitStringProcess)parentProcess;
                return parent.words.Select(word => new DoubleWordProcess(word));
            })
                .Do(parent => {
                    return new DisplayWordProcess(((DoubleWordProcess)parent).OutputWord);
                })
                /* .DoSeveral(parentProcess => {
                    SplitStringProcess parent = (SplitStringProcess)parentProcess;
                    //Debug.Log(parent);
                    return parent.words.Select(word => new DisplayWordProcess(word));
                }) */
            .Out()
        .Out()
        ;

        return composite;
    }
}