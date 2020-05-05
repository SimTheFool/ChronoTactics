using UnityEngine;
using System.Collections.Generic;

public class CombatGameState : IGameState
{
    private Level levelData = null;
    private GameStateMachine gameStateMachine = null;

    private TilemapManager tilemapManager = null;
    private TimelineController timelineController = null;

    public CombatGameState(GameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;

        this.tilemapManager = DependencyLocator.getTilemapManager();
        this.timelineController = DependencyLocator.getTimelineController();
    }

    public void In()
    {
        if(this.levelData == null) return;

        foreach(Actor actor in this.levelData.actors)
        {
            Actor newActor = GameObject.Instantiate(actor);
        }
        GameObject.Instantiate(levelData.map).transform.parent = this.tilemapManager.transform;

        this.tilemapManager.Init(new List<ITilemapAgent>(this.levelData.actors));
        this.timelineController.Init(new List<ITimelineAgent>(this.levelData.actors));
    }

    public void Update()
    {
        bool combatEnded = this.timelineController.Process();
        if(combatEnded) this.gameStateMachine.SetMenuState();
    }

    public void Out()
    {

    }

    public void SetLevelData(Level level)
    {
        this.levelData = level;
    }
}