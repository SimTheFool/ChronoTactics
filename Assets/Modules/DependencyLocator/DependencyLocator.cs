using UnityEngine;

public class DependencyLocator : MonoBehaviour
{

    private static DependencyLocator instance;

    [SerializeField]
    private TimelineController timelineController = null;

    private Finder<TileFacade> pathfinder = null;

    [SerializeField]
    private TilemapManager tilemapManager = null;

    [SerializeField]
    private SkillQueueResolver skillQueueResolver = null;

    private ControlsMapper controlsMapper = null;

    [SerializeField]
    private CombatControls combatControls = null;


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

    public static TimelineController getTimelineController()
    {
        return instance.timelineController;
    }

    public static Finder<TileFacade> getPathfinder()
    {
        if(instance.pathfinder == null)
        {
            TilemapManager tilemap = DependencyLocator.getTilemapManager();
            instance.pathfinder = new Finder<TileFacade>(tilemap.TilesMap);
        }
        return instance.pathfinder;
    }

    public static TilemapManager getTilemapManager()
    {
        return instance.tilemapManager;
    }

    public static SkillQueueResolver GetSkillQueueResolver()
    {
        return instance.skillQueueResolver;
    }

    public static ControlsMapper GetControlsMapper()
    {
        if(instance.controlsMapper == null)
        {
            instance.controlsMapper = new ControlsMapper();
        }

        return instance.controlsMapper;
    }

    public static CombatControls GetCombatControls()
    {
        return instance.combatControls;
    }
}
