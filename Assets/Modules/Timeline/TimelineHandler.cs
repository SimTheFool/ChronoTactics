using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineHandler : MonoBehaviour
{

    private List<Actor> gameActors;

    private int timelineMaxLength = 6;
    private int turnCount = 0;
    private List<TimelineActor> timelineActors;
    public Dictionary<int, Turn> timeline;


    void Start()
    {
        this.fetchGameActors();
        this.InitializeTimeline();

        foreach(KeyValuePair<int, Turn> pair in this.timeline)
        {
            Debug.Log(pair.Key);
            Debug.Log(pair.Value);
        }
    }

    void Update()
    {

    }

    private void InitializeTimeline()
    {
        this.timeline = new Dictionary<int, Turn>();
        while(this.Count() <= this.timelineMaxLength)
        {
            this.turnCount ++;

            Turn turn = new Turn();
            this.timelineActors = turn.UpdateAllActors(this.timelineActors);
            this.timeline.Add(this.turnCount, turn);
        }
    }

    private void fetchGameActors()
    {
        this.gameActors = new List<Actor>();
        this.gameActors.Add(new Actor("Roger", 50));  
        this.gameActors.Add(new Actor("Paulette", 100));
        this.gameActors.Add(new Actor("Bernard", 150));

        this.timelineActors = new List<TimelineActor>();
        foreach(Actor actor in gameActors)
        {
            this.timelineActors.Add(new TimelineActor(actor));
        }
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
