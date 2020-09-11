using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skill")]
public class Skill : ScriptableObject
{
    public string Label;
    public List<SkillProcessDatas> SkillDatas;

    public Skill(Func<SkillInput, SkillComposite> buildSkillCbk, string skillLabel)
    {

    }

    public void Init(SkillInput input)
    {

    }

    public bool Process()
    {
        return false;
    }
}