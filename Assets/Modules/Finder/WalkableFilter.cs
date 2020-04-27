public class WalkableFilter : IFilter
{
    public bool IsAccessible(IPositionnable elem)
    {
        TileFacade tile = (TileFacade)elem;
        GroundTile tileBase =(GroundTile) tile.GroundTile;
        return tileBase.isWalkable;
    }
}
