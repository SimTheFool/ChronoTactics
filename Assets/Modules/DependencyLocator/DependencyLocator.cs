using UnityEngine;
using UnityEngine.Tilemaps;

public class DependencyLocator : MonoBehaviour
{

    private static DependencyLocator instance;

    public TimelineHandler timelineHandler = null;
    private Finder<TileFacade> pathfinder = null;
    public TilemapFacade tilemap = null;

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
        return instance.timelineHandler;
    }

    public static Finder<TileFacade> getPathfinder()
    {
        if(instance.pathfinder == null)
        {
            TilemapFacade tilemap = DependencyLocator.getTilemap();
            instance.pathfinder = new Finder<TileFacade>(tilemap.tilesMap);
        }
        return instance.pathfinder;
    }

    public static TilemapFacade getTilemap()
    {
        return instance.tilemap;
    }
}
