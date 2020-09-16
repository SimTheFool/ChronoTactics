using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skill")]
public class Skill : ScriptableObject
{
    public string Label;
    public List<SkillNodeDatas> SkillNodeDatas;

    public void Init(SkillInput input)
    {

    }

    public bool Process()
    {
        return false;
    }
}