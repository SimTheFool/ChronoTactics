public interface ITimelineAgentsCollectionManager
{
    ITimelineAgent CurrentAgent {get;}
    int Count {get;}

    void MoveToNextAgent();
    void Add(ITimelineAgent agent);
    void Remove(ITimelineAgent agent);
}