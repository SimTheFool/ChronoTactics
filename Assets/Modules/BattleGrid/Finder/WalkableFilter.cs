public class WalkableFilter : IFilter
{
    public bool IsAccessible(IPositionnable elem)
    {
        Cell cell = (Cell)elem;
        return cell.getPrototype().isWalkable;
    }
}
