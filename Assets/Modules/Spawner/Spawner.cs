using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Actor Spawn(Actor actor)
    {
        Actor newActor = GameObject.Instantiate(actor);
        return newActor;
    }

    public void Spawn(TilemapIdentifier map)
    {
        GameObject.Instantiate(map).transform.parent = DependencyLocator.getTilemapManager().transform;
    }
}