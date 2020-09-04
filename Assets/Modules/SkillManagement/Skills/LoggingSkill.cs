using System.Collections.Generic;
using System;
using System.Linq;

public class LoggingSkill : Skill
{
    public LoggingSkill() : base(null, "Logging")
    {
        this.buildSkillCbk = this.BuildSkill;
    }

    private SkillComposite BuildSkill(SkillInput input)
    {

        SkillComposite composite = new SkillComposite(new DoubleWordProcess("foo"));       

        Func<SkillProcess, DisplayWordProcess> onDoubleWord_DisplayWord = (parentProcess) => {
            DoubleWordProcess comp = (DoubleWordProcess)parentProcess;
            return new DisplayWordProcess(comp.outputWord);
        };

        Func<SkillProcess, DoubleWordProcess> onDoubleWord_DoubleWord = (parentProcess) => {
            DoubleWordProcess comp = (DoubleWordProcess)parentProcess;
            return new DoubleWordProcess(comp.outputWord);
        };

        composite
        .In(parentProcess => new SplitStringProcess("abc#def.ghi.jkl#mno", '.'))
                .InSeveral(parentProcess => {
                    SplitStringProcess parent = (SplitStringProcess)parentProcess;
                    return parent.words.Select(word => new SplitStringProcess(word, '#'));
                })
                    .DoSeveral(parentProcess => {
                        SplitStringProcess parent = (SplitStringProcess)parentProcess;
                        return parent.words.Select(word => new DisplayWordProcess(word));
                    })
                .Out()
        .Out()
        ;

        return composite;
    }
}