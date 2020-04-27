using UnityEngine;

[RequireComponent(typeof(Actor))]
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

    void Update()
    {
        this.currentState.BehaviourUpdate();
    }

    public void SetStateNotActive()
    {
        this.SetState(this.notActive);
    }

    public void SetStateActive()
    {
        if(this.actor.Playable)
        {
            this.SetState(this.activePlayable);
            return;
        }

        this.SetState(this.activeAI);
    }

    private void SetState(IBehaviourState state)
    {
        if(this.currentState != null) this.currentState.Out();
        this.currentState = state;
        this.currentState.In();
    }
}