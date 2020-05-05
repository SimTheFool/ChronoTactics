using UnityEngine;

public interface ITilemapAgent
{
    TileFacade GetTile();

    void SetTile(TileFacade tile);
}