public enum TimelineAgentType {Actor, Effect}


public interface ITimelineAgent
{
    TimelineAgentType agentType {get;}
    string Name {get;}
    int UniqId {get;}

    int Atb {get; set;}
    int Speed {get;}

    void OnBeginPass();
    void OnEndPass();
}