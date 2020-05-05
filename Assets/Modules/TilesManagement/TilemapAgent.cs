using UnityEngine;

public class TilemapAgent : MonoBehaviour
{
    private TileFacade tile = null;
    private Actor actor = null;
    public Actor Actor
    {
        get
        {
            return this.actor;
        }
    }

    private void Start()
    {
        this.actor = this.GetComponent<Actor>();
        DependencyLocator.getTilemapFacade().LocateAgentOnStartingTile(this);
    }

    public TileFacade GetTile()
    {
        return this.tile;
    }

    public void SetTile(TileFacade tile)
    {
        this.tile = tile;
        this.transform.position = tile.WorldPos;
        tile.Agent = this;
    }
}