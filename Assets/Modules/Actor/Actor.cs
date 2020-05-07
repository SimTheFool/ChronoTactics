using UnityEngine;

[RequireComponent(typeof(BehaviourStateMachine), typeof(ActorStats), typeof(ActorSkills))]
public class Actor: MonoBehaviour, ITilemapAgent, ITimelineAgent
{
    [SerializeField]
    private bool playable = true;
    public bool Playable => this.playable;

    [SerializeField]
    private string actorName = "";
    public string Name => this.actorName;

    private BehaviourStateMachine behaviourStateMachine = null;

    private ActorStats stats = null;
    public ActorStats Stats => this.stats;

    private ActorSkills skills = null;
    public ActorSkills Skills => this.skills;

    void Awake() 
    {
        this.behaviourStateMachine = this.GetComponent<BehaviourStateMachine>();
        this.stats = this.GetComponent<ActorStats>();
        this.skills = this.GetComponent<ActorSkills>();
    }

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
    string ITimelineAgent.Name => this.actorName;

    public int Atb
    {
        get => this.Stats.Atb;
        set => this.Stats.Atb = value;
    }

    public int Speed => this.Stats.Speed;
    public int UniqId => this.GetInstanceID();

    public void OnBeginPass()
    {
        if(this.playable)
        {
            this.behaviourStateMachine.SetStateActivePlayable();
            return;
        }

        this.behaviourStateMachine.SetStateActiveAI();        
    }

    public void OnEndPass()
    {
        this.behaviourStateMachine.SetStateNotActive();
    }
}