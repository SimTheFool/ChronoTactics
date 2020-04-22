using UnityEngine;

public class TimelineAgent : MonoBehaviour 
{
    private Actor actor;
    public Actor Actor
    {
        get
        {
            return this.actor;
        }
    }

    private int atb = 0;
    public int Atb
    {
        get
        {
            return this.atb;
        }

        set
        {
            this.atb = value;
        }
    }

    public int Speed
    {
        get
        {
            return this.actor.Speed;
        }
    }

    public string Name
    {
        get
        {
            return this.actor.Name;
        }
    }

    private void Start()
    {
        this.actor = this.GetComponent<Actor>();
        DependencyLocator.getTimelineHandler().AddOrUpdateAgent(this);
    }

    /* private void OnDestroy() {
        DependencyLocator.getTimelineHandler().RemoveAgent(this);
    } */

    public void EnablePlaying()
    {
        Debug.Log($"{this.Name} can play");
    }

    public void DisablePlaying()
    {
        //Debug.Log("set can't play on actor");
    }
}