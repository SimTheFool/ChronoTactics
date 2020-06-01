using UnityEngine;

public class ActorFacade: BaseActor, ITilemapAgent, ITimelineAgent
{
    [SerializeField]
    private bool playable = true;
    public bool Playable => this.playable;

    [SerializeField]
    private string actorName = "";
    public string Name => this.actorName;

    new public ActorSkills Skills => base.Skills;
    new public ActorStats Stats => base.Stats;
    new public ActorEvents Events => base.Events;
    new public ActorBehaviour Behaviour => base.Behaviour;

    // ITilemapAgent implementation.
    private TileFacade tile;

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

    //ITimelineAgent implementation
    public TimelineAgentType agentType => TimelineAgentType.Actor;
    
    string ITimelineAgent.Name => this.actorName;

    public int Atb
    {
        get => this.Stats.Atb;
        set => this.Stats.Atb = value;
    }

    public int Speed => this.Stats.Speed;

    public (int groupId, int selfId) UniqId => (this.GetInstanceID(), 0);

    public void OnBeginPass()
    {
        Debug.Log(this.Name);

        if(this.playable)
        {
            this.Behaviour.SetStateActivePlayable();
            return;
        }

        this.Behaviour.SetStateActiveAI();        
    }

    public void OnEndPass()
    {
        this.Behaviour.SetStateNotActive();
    }
}