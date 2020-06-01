public interface ITimelineAgentsCollectionManager
{
    ITimelineAgent CurrentAgent {get;}

    void MoveToNextAgent();
    void Add(ITimelineAgent agent);
    void Remove(ITimelineAgent agent);
}