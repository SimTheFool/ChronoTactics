using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

public class Actor: MonoBehaviour
{
    [SerializeField]
    private Skill moveSkill = null;
    public Skill MoveSkill
    {
        get
        {
            return this.moveSkill;
        }
    }

    [SerializeField]
    private Skill endTurnSkill = null;
    public Skill EndTurnSkill
    {
        get
        {
            return this.endTurnSkill;
        }
    }

    [SerializeField]
    private List<Skill> otherSkills = null;
    public List<Skill> OtherSkills
    {
        get
        {
            return this.otherSkills;
        }
    }

    public List<Skill> Skills
    {
        get
        {
            List<Skill> skills = new List<Skill>(this.otherSkills);
            skills.Add(this.moveSkill);
            skills.Add(this.endTurnSkill);
            return skills;
        }
    }

    private Skill selectedSkill = null;
    public Skill SelectedSkill
    {
        get
        {
            return this.selectedSkill;
        }
        set
        {
            this.selectedSkill = value;
        }
    }

    private TilemapAgent tilemapAgent;

    public TileFacade Tile
    {
        get
        {
            return this.tilemapAgent.GetTile();
        }
        set
        {
            this.tilemapAgent.SetTile(value);
        }
    }

    [SerializeField]
    private int health = 100;
    public int Health
    {
        get
        {
            return this.health;
        }

        set
        {
            this.health = value;
        }
    }

    [SerializeField]
    private bool playable = true;
    public bool Playable
    {
        get
        {
            return this.playable;
        }
    }

    [SerializeField]
    private string actorName = "";
    public string Name
    {
        get
        {
            return this.actorName;
        }
    }

    [SerializeField]
    private int speed = 100;
    public int Speed
    {
        get
        {
            return this.speed;
        }
    }

    void Start()
    {
        this.tilemapAgent = this.GetComponent<TilemapAgent>();
    }

    public override string ToString()
    {
        return this.actorName;
    }
}