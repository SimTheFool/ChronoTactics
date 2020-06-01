using UnityEngine;

[RequireComponent(typeof(PassesManager), typeof(TurnBreaksManager), typeof(TimelineEvents))]
[RequireComponent(typeof(TimelineController))]
public class TimelineComponentsBridge : MonoBehaviour
{
    private PassesManager passes = null;
    protected PassesManager Passes
    {
        get
        {
            if(this.passes == null)
                this.passes = this.GetComponent<PassesManager>();
            return this.passes;
        }
    }

    private TurnBreaksManager turnBreaks = null;
    protected TurnBreaksManager TurnBreaks
    {
        get
        {
            if(this.turnBreaks == null)
                this.turnBreaks = this.GetComponent<TurnBreaksManager>();
            return this.turnBreaks;
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