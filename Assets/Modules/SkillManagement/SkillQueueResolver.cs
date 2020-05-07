using UnityEngine;
using System.Collections.Generic;

public class SkillQueueResolver : MonoBehaviour
{
    private Queue<KeyValuePair<SkillInput, Skill>> skills = new Queue<KeyValuePair<SkillInput, Skill>>();
    private Skill currentSkill = null;
    private SkillInput currentInput = null;

    public void AddSkill(SkillInput input, Skill skill)
    {
        this.skills.Enqueue(new KeyValuePair<SkillInput, Skill>(input, skill));
    }

    public void Process()
    {
        bool done = true;

        if(this.currentSkill != null)
        {
            done = this.currentSkill.Process(this.currentInput);
        }

        if(done == true)
        {
            this.MoveToNextSkill();
        }
    }

    private void MoveToNextSkill()  
    {
        if(this.skills.Count == 0)
        {
            this.currentSkill = null;
            return;
        }

        KeyValuePair<SkillInput, Skill> skill = this.skills.Dequeue();
        this.currentSkill = skill.Value;
        this.currentInput = skill.Key;
        this.currentSkill.Init(this.currentInput);
    }
}