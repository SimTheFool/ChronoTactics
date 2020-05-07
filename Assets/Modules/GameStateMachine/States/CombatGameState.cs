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

    public CombatGameState(GameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;

        this.tilemapManager = DependencyLocator.getTilemapManager();
        this.timelineController = DependencyLocator.getTimelineController();
        this.skillQueueResolver = DependencyLocator.GetSkillQueueResolver();
        this.combatActionsMapping = DependencyLocator.GetActionsMapper<CombatActionsMapping>();
    }

    public void In()
    {
        if(this.levelData == null) return;

        List<Actor> newActors = new List<Actor>();
        foreach(Actor actor in this.levelData.actors)
        {
            Actor newActor = GameObject.Instantiate(actor);
            newActors.Add(newActor);
        }
        GameObject.Instantiate(levelData.map).transform.parent = this.tilemapManager.transform;

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