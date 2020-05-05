﻿using UnityEngine;

public class DependencyLocator : MonoBehaviour
{

    private static DependencyLocator instance;

    [SerializeField]
    private TimelineHandler timelineHandler = null;

    private Finder<TileFacade> pathfinder = null;

    [SerializeField]
    private TilemapFacade tilemapFacade = null;

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

    public static TimelineHandler getTimelineHandler()
    {
        return instance.timelineHandler;
    }

    public static Finder<TileFacade> getPathfinder()
    {
        if(instance.pathfinder == null)
        {
            TilemapFacade tilemap = DependencyLocator.getTilemapFacade();
            instance.pathfinder = new Finder<TileFacade>(tilemap.tilesMap);
        }
        return instance.pathfinder;
    }

    public static TilemapFacade getTilemapFacade()
    {
        return instance.tilemapFacade;
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
