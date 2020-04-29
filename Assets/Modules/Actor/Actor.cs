using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Actor: MonoBehaviour
{
    private List<KeyValuePair<string, UnityAction>> actions = new List<KeyValuePair<string, UnityAction>>();
    public List<KeyValuePair<string, UnityAction>> Actions
    {
        get
        {
            return this.actions;
        }
    }

    private TileFacade tile = null;
    public TileFacade Tile
    {
        get
        {
            return this.tile;
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
        this.actions.Add(new KeyValuePair<string, UnityAction>($"action1 {this.actorName}", () => {
            Debug.Log("action 1");
        }));

        this.actions.Add(new KeyValuePair<string, UnityAction>($"action2 {this.actorName}", () => {
            Debug.Log("action 2");
        }));
    }

    public override string ToString()
    {
        return this.actorName;
    }
}