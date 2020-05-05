using UnityEngine;
using System.Collections.Generic;

public class Actor: MonoBehaviour, ITilemapAgent
{
    private TileFacade tile;

    public TileFacade GetTile()
    {
        return this.tile;
    }

    public void SetTile(TileFacade tile)
    {
        this.tile = tile;
        this.transform.position = tile.WorldPos;
        tile.Agent = this;
    } 

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
}