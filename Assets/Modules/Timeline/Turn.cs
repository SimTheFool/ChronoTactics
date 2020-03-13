using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    private List<TimelineActor> inputActors;
    private List<KeyValuePair<Actor, float>> priorities;

    public List<TimelineActor> UpdateAllActors(List<TimelineActor> actors)
    {
        this.inputActors = new List<TimelineActor>(actors);
        this.priorities = new List<KeyValuePair<Actor, float>>();

        List<TimelineActor> tempActors = new List<TimelineActor>(this.inputActors);

        for(int i = 0; i < tempActors.Count; i++)
        {
            tempActors[i] = this.EvaluatePriorities(tempActors[i]);
        }

        this.SortPriorities();
        return tempActors;
    }

    private TimelineActor EvaluatePriorities(TimelineActor actor)
    {
        int finalAtb = actor.atb + actor.gameActor.speed;
        int canPlayNb = finalAtb / 100;

        for(int i = 1; i <= canPlayNb; i++)
        {
            float priority = ((i * 100) - actor.atb) / (float)actor.gameActor.speed;
            this.priorities.Add(new KeyValuePair<Actor, float>(actor.gameActor, priority));
        }
        
        actor.atb = finalAtb % 100;
        return actor;
    }

    private void SortPriorities()
    {
        this.priorities.Sort((x, y) => {
            return x.Value.CompareTo(y.Value);
        });
    }

    public int Count()
    {
        return priorities.Count;
    }

    public override string ToString()
    {
        string msg = "";
        
        msg += "Actors ::";
        foreach(TimelineActor actor in this.inputActors)
        {
            msg += " " + actor.ToString();
        }

        msg += "\n";

        msg += "Priorities ::";
        foreach(KeyValuePair<Actor, float> pair in this.priorities)
        {
            msg += " " + pair.Key.ToString() + " " + pair.Value.ToString() + "~~";
        }

        return msg;
    }

    // @TODO data struct to encapsulate actor index with priority.

    // 
}