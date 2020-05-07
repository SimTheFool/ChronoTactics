using UnityEngine;
using System.Collections.Generic;

public class ActorSkills : MonoBehaviour
{
    public List<Skill> Skills
    {
        get
        {
            List<Skill> skills = new List<Skill>(this.others);
            skills.Add(this.move);
            skills.Add(this.endTurn);
            return skills;
        }
    }

    [SerializeField]
    private Skill move = null;
    public Skill Move => this.move;

    [SerializeField]
    private Skill endTurn = null;
    public Skill EndTurn => this.endTurn;

    [SerializeField]
    private List<Skill> others = null;
    private List<Skill> Others => this.others;

    private Skill selected = null;
    public Skill Selected
    {
        get => this.selected;
        set => this.selected = value;
    }
}