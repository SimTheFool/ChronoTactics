using UnityEngine;

[RequireComponent(typeof(TurnsManager), typeof(BreaksManager), typeof(TimelineEvents))]
[RequireComponent(typeof(TimelineController))]
public class TimelineComponentsBridge : MonoBehaviour
{
    private TurnsManager turns = null;
    protected TurnsManager Turns
    {
        get
        {
            if(this.turns == null)
                this.turns = this.GetComponent<TurnsManager>();
            return this.turns;
        }
    }

    private BreaksManager breaks = null;
    protected BreaksManager Breaks
    {
        get
        {
            if(this.breaks == null)
                this.breaks = this.GetComponent<BreaksManager>();
            return this.breaks;
        }
    }

    private TimelineEvents events = null;  
    protected TimelineEvents Events
    {
        get
        {
            if(this.events == null)
                this.events = this.GetComponent<TimelineEvents>();
            return this.events;
        }
    }

    private TimelineController controller = null;
    protected TimelineController Controller
    {
        get
        {
            if(this.controller == null)
                this.controller = this.GetComponent<TimelineController>();
            return this.controller;
        }
    }
}