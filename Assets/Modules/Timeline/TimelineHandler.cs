using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineHandler : MonoBehaviour
{

    private List<Actor> gameActors = new List<Actor>();

    private int timelineMaxLength = 6;
    private int turnCount = 0;
    public Dictionary<int, Turn> timeline = new Dictionary<int, Turn>();

    void Start()
    {
        this.InitializeTimeline();
    }

    private void InitializeTimeline()
    {
        List<TimelineActor> actors = new List<TimelineActor>();
        foreach(Actor actor in this.gameActors)
        {
            actors.Add(new TimelineActor(actor));
        }

        while(this.Count() <= this.timelineMaxLength)
        {
            this.turnCount ++;
            Turn turn = new Turn();

            actors = turn.UpdateAllActors(actors);
            this.timeline.Add(this.turnCount, turn);
        }
    }

    public void registerActor(Actor actor)
    {
        this.gameActors.Add(actor);
    }

    public int Count()
    {
        int count = 0;
        foreach(Turn turn in this.timeline.Values)
        {
            count += turn.Count();
        }
        return count;
    }
}
