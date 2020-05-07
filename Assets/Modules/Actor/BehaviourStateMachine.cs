using UnityEngine;

public class BehaviourStateMachine : MonoBehaviour
{
    private Actor actor = null;

    private IBehaviourState currentState = null;

    private IBehaviourState notActive = null;
    private IBehaviourState activePlayable = null;
    private IBehaviourState activeAI = null;

    void Start()
    {
        this.actor = this.GetComponent<Actor>();

        this.notActive = new NotActiveState();
        this.activePlayable = new ActivePlayableState(this.actor);
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