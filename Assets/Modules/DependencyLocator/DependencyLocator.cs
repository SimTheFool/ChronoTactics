using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

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

    [SerializeField]
    private Spawner spawner = null;

    private Dictionary<Type, IInputActionCollection> actionsMapper = new Dictionary<Type, IInputActionCollection>();


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

    public static TMapperType GetActionsMapper<TMapperType>() where TMapperType : IInputActionCollection
    {
        IInputActionCollection mapper = null;
        if(!instance.actionsMapper.TryGetValue(typeof(TMapperType), out mapper))
        {
            mapper = (IInputActionCollection)Activator.CreateInstance(typeof(TMapperType));
            instance.actionsMapper.Add(typeof(TMapperType), mapper);
        }

        return (TMapperType)mapper;
    }

    public static Spawner GetSpawner()
    {
        return instance.spawner;
    }
}
