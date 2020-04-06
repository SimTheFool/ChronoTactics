using UnityEngine;
using System.Collections.Generic;

public class DependencyLocator : MonoBehaviour
{

    private static DependencyLocator instance;

    private static TimelineHandler timelineHandler = null;
    private static Finder<Cell> pathfinder = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public static TimelineHandler getTimelineHandler()
    {
        if(timelineHandler == null)
        {
            timelineHandler = instance.gameObject.GetComponent<TimelineHandler>();
        }

        return timelineHandler;
    }

    public static Finder<Cell> getPathfinder()
    {
        if(pathfinder == null)
        {
            pathfinder = new Finder<Cell>(Cell.grid);
        }

        return pathfinder;
    }
}
