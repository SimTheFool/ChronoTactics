using UnityEngine;
using System.Collections.Generic;

public class CombatGameState : IGameState
{
    private Level levelData = null;
    private GameStateMachine gameStateMachine = null;

    private TilemapManager tilemapManager = null;
    private TimelineController timelineController = null;
    private SkillQueueResolver skillQueueResolver = null;
    private CombatActionsMapping combatActionsMapping = null;
    private Spawner spawner = null;

    public CombatGameState(GameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;

        this.tilemapManager = DependencyLocator.getTilemapManager();
        this.timelineController = DependencyLocator.getTimelineController();
        this.skillQueueResolver = DependencyLocator.GetSkillQueueResolver();
        this.combatActionsMapping = DependencyLocator.GetActionsMapper<CombatActionsMapping>();
        this.spawner = DependencyLocator.GetSpawner();
    }

    public void In()
    {
        if(this.levelData == null) return;

        List<ActorFacade> newActors = new List<ActorFacade>();
        foreach(ActorFacade actor in this.levelData.actors)
        {
            ActorFacade newActor = this.spawner.Spawn(actor);
            newActors.Add(newActor);
        }
        this.spawner.Spawn(levelData.map);

        this.tilemapManager.Init(new List<ITilemapAgent>(newActors));
        this.timelineController.Init(new List<ITimelineAgent>(newActors));
        
        this.combatActionsMapping.Enable();
    }

    public void Process()
    {
        bool combatEnded = this.timelineController.Process();
        this.skillQueueResolver.Process();


        if(combatEnded) this.gameStateMachine.SetMenuState();
    }

    public void Out()
    {
        this.combatActionsMapping.Disable();
    }

    public void SetLevelData(Level level)
    {
        this.levelData = level;
    }
}