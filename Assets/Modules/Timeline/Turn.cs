using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    private List<TimelineActor> inputActors;
    private List<KeyValuePair<int, float>> priorities;

    // Record all timeline actors state for this turn, process their priority for this turn, and output new timeline actors state
    public List<TimelineActor> UpdateAllActors(List<TimelineActor> actors)
    {
        this.inputActors = new List<TimelineActor>(actors);
        this.priorities = new List<KeyValuePair<int, float>>();

        List<TimelineActor> outputActors = new List<TimelineActor>();

        for(int i = 0; i < inputActors.Count; i++)
        {
            outputActors.Add(this.EvaluateActorPriorities(i));
        }

        this.SortPriorities();
        return outputActors;
    }

    // From an index, get a timeline actor state from the inputActors, evaluate its priorities, and return a new timeline actor state
    private TimelineActor EvaluateActorPriorities(int actorIndex)
    {
        TimelineActor actor = this.inputActors[actorIndex];
        int finalAtb = actor.getAtb() + actor.getSpeed();
        int canPlayNb = finalAtb / 100;

        for(int i = 1; i <= canPlayNb; i++)
        {
            float priority = ((i * 100) - actor.getAtb()) / (float)actor.getSpeed();
            this.priorities.Add(new KeyValuePair<int, float>(actorIndex, priority));
        }
        
        actor.setAtb(finalAtb % 100);
        return actor;
    }

    private void SortPriorities()
    {
        this.priorities.Sort((x, y) => {
            int comparison = x.Value.CompareTo(y.Value);
            
            if(comparison == 0)
            {
                int speedX = this.inputActors[x.Key].getSpeed();
                int speedY = this.inputActors[y.Key].getSpeed();

                return speedX.CompareTo(speedY) * -1;
            }

            return comparison;
        });
    }

    public int Count()
    {
        return priorities.Count;
    }

    public override string ToString()
    {
        string msg = "Turn infos";

        msg += "\n Actors ::";
        foreach(TimelineActor actor in this.inputActors)
        {
            msg += $" {actor.ToString()} [atb: {actor.getAtb()}]";
        }

        msg += "\n \n Priorities ::";
        foreach(KeyValuePair<int, float> pair in this.priorities)
        {
            TimelineActor actor = this.inputActors[pair.Key];
            msg += $"\n {actor} [priority: {pair.Value.ToString()}, speed: {actor.getSpeed()}]";
        }

        return msg;
    }
}