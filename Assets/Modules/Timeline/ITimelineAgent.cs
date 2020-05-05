public interface ITimelineAgent
{
    string Name {get;}
    int UniqId {get;}

    int Atb {get; set;}
    int Speed {get;}

    void OnBeginPass();
    void OnEndPass();
}