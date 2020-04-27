using UnityEngine;

[RequireComponent(typeof(Actor))]
[RequireComponent(typeof(BehaviourStateMachine))]
public class TimelineAgent : MonoBehaviour 
{
    private BehaviourStateMachine actorBehaviour = null;

    private Actor actor = null;
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
        this.actorBehaviour = this.GetComponent<BehaviourStateMachine>();
        DependencyLocator.getTimelineHandler().AddOrUpdateAgent(this);
    }

    public void EnablePlaying()
    {
        this.actorBehaviour.SetStateActive();
    }

    public void DisablePlaying()
    {
        this.actorBehaviour.SetStateNotActive();
    }
}