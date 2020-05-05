using UnityEngine;
using System.Collections.Generic;

public class CombatGameState : IGameState
{
    private Level levelData = null;

    public void In()
    {
        if(this.levelData == null) return;

        // Instantiating agents, may be Spawner class resposability.
        List<ITilemapAgent> agents = new List<ITilemapAgent>();
        foreach(Actor actor in levelData.actors)
        {
            Actor newActor = GameObject.Instantiate(actor);
            agents.Add(newActor);
        }

        // Instantiating map, may be Spawner class resposability.
        TilemapFacade tilemapFacade = DependencyLocator.getTilemapFacade();
        GameObject.Instantiate(levelData.map).transform.parent = tilemapFacade.transform;

        // Initializing TileFacade.
        tilemapFacade.Init(agents);
    }

    public void Update()
    {
        
    }

    public void Out()
    {

    }

    public void SetLevelData(Level level)
    {
        this.levelData = level;
    }
}