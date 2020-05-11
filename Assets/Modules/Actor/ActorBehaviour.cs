using UnityEngine;

public class ActorBehaviour : BaseActor
{

    private IBehaviourState currentState = null;

    private IBehaviourState notActive = null;
    private IBehaviourState activePlayable = null;
    private IBehaviourState activeAI = null;

    void Awake()
    {
        this.notActive = new NotActiveState();
        this.activePlayable = new ActivePlayableState(this.Facade);
        this.activeAI = new ActiveAIState();

        this.SetStateNotActive();
    }

    private void SetState(IBehaviourState state)
    {
        if(this.currentState != null) this.currentState.Out();
        this.currentState = state;
        this.currentState.In();
    }

    public void SetStateNotActive()
    {
        this.SetState(this.notActive);
    }

    public void SetStateActivePlayable()
    {

        this.SetState(this.activePlayable);
    }

    public void SetStateActiveAI()
    {
        this.SetState(this.activeAI);
    }
}