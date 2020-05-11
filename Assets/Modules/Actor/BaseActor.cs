using UnityEngine;

[RequireComponent(typeof(ActorBehaviour), typeof(ActorEvents), typeof(ActorSkills))]
[RequireComponent(typeof(ActorFacade), typeof(ActorStats))]
public class BaseActor : MonoBehaviour
{
    private ActorBehaviour behaviour = null;
    protected ActorBehaviour Behaviour
    {
        get
        {
            if(this.behaviour == null)
            {
                this.behaviour = this.GetComponent<ActorBehaviour>();
            }
            return this.behaviour;
        }
    }

    private ActorEvents events = null;
    protected ActorEvents Events
    {
        get
        {
            if(this.events == null)
            {
                this.events = this.GetComponent<ActorEvents>();
            }
            return this.events;
        }
    }

    private ActorStats stats = null;
    protected ActorStats Stats
    {
        get
        {
            if(this.stats == null)
            {
                this.stats = this.GetComponent<ActorStats>();
            }
            return this.stats;
        }
    }

    private ActorSkills skills = null;
    protected ActorSkills Skills
    {
        get
        {
            if(this.skills == null)
            {
                this.skills = this.GetComponent<ActorSkills>();
            }
            return this.skills;
        }
    }

    private ActorFacade facade = null;
    public ActorFacade Facade
    {
        get
        {
            if(this.facade == null)
            {
                this.facade = this.GetComponent<ActorFacade>();
            }
            return this.facade;
        }
    }
}