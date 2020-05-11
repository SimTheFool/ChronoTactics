using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ActorFacade Spawn(ActorFacade actor)
    {
        ActorFacade newActor = GameObject.Instantiate(actor);
        return newActor;
    }

    public void Spawn(TilemapIdentifier map)
    {
        GameObject.Instantiate(map).transform.parent = DependencyLocator.getTilemapManager().transform;
    }
}