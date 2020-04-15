using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimelineHandler : MonoBehaviour
{
    private TimelineAgent currentAgent = null;
    private float timer = 0;
    [SerializeField]
    private float secondsPerAgent = 15f;

    private HashSet<TimelineAgent> agentsAtStart = new HashSet<TimelineAgent>();
    private Turn turn = null;
    [SerializeField]
    private int previewedElemMinNb = 20;
    
    private bool isInit = true;

    private void Awake() {
        this.timer = 0;
        this.isInit = true;
        this.turn = null;
    }

    private void Update() {
        this.PreviewTurns();
        this.isInit = false;
        this.PlayCurrentTurn();
    }

    private void PlayCurrentTurn()
    {
        // We update the timer for current actor.
        this.timer -= Time.deltaTime;
        if(timer > 0)
        {
            return;
        }

        // If time is up, we move to next agent for this turn.
        TimelineAgent nextAgent = this.turn.MoveToNextAgent();

        // If there is no next agent, we must change turn.
        if(nextAgent == null)
        {
            Turn nextTurn = this.turn.MoveToNextTurn();

            // If there no next turn, we end combat.
            if(nextTurn == null)
            {
                Debug.Log("end of combat");
            }

            // If there is, we get next agent of that turn.
            this.turn = nextTurn;
            nextAgent = this.turn.MoveToNextAgent();
        }

        if(this.currentAgent != null)
        {
            this.currentAgent.DisablePlaying();
        }
        this.currentAgent = nextAgent;
        this.currentAgent.EnablePlaying();

        this.timer = this.secondsPerAgent;
    }

    private void PreviewTurns()
    {
        // At initialisation we create the first turn from the registered agents.
        if(this.turn == null && this.isInit)
        {
            Turn firstTurn = new Turn(this.agentsAtStart);
            this.turn = firstTurn;
            return;
        }

        // After initialization, we deduce the other turns from the first one.
        while(this.turn.Count < this.previewedElemMinNb)
        {
            this.turn.NewTurn();
        }
    }

    public void AddOrUpdateAgent(TimelineAgent agent)
    {
        // At initialization we register agents in a dedicated list for the first turn.
        if(this.isInit)
        {
            this.agentsAtStart.Add(agent);
            return;
        }

        // After initialization, we only update the current turn, and the others will update in cascade-like effect.
        this.turn.AddOrUpdateAgent(agent);
    }

    public void RemoveAgent(TimelineAgent agent)
    {
        // At initialization we unregister agents in the dedicated list for the first turn.
        if(this.isInit)
        {
            this.agentsAtStart.Remove(agent);
            return;
        }

        // After initialization, we only update the current turn, and the others will update in cascade-like effect.
        this.turn.RemoveAgent(agent);
    }

    private void OnGUI()
    {
        int dim = 20;

        GUI.Label(new Rect(10, 10, 100, 20), $"{this.timer}");

        if(this.turn == null) return;

        Dictionary<int, List<TimelineAgent>> agentsPerTurn = this.turn.RemainingAgentsPerTurn;

        foreach(int turnNb in agentsPerTurn.Keys)
        {
            int k = 0;
            foreach(TimelineAgent agent in agentsPerTurn[turnNb])
            {
                k++;
                GUI.Label(new Rect(100 + k*dim, 10 + turnNb*dim*3, dim, dim), $"{turnNb}");
                GUI.Label(new Rect(100 + k*dim, 10 + turnNb*dim*3 + dim, dim, dim), $"{agent.Name.Substring(0,2)}");
                GUI.Label(new Rect(100 + k*dim, 10 + turnNb*dim*3 + 2*dim, dim, dim), $"{agent.Speed}");
            }
        }
    }
}
