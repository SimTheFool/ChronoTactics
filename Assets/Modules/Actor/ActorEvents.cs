using System.Collections.Generic;
using System;

public class ActorEvents : BaseActor
{
    public static HashSet<Action> eventQueue = new HashSet<Action>();

    public static event Action<ActorFacade, Action<Skill>> onPlayableStateEnabled;
    public static void OnPlayableStateEnabled(ActorFacade actor, Action<Skill> selectSkillAction)
    {
        eventQueue.Add(() => onPlayableStateEnabled?.Invoke(actor, selectSkillAction));
    }

    public static event Action<ActorFacade> onPlayableStateDisabled;
    public static void OnPlayableStateDisabled(ActorFacade actor)
    {
        eventQueue.Add(() => onPlayableStateDisabled?.Invoke(actor));
    }

    void Update()
    {
        if(eventQueue.Count <= 0)
            return;

        foreach(Action action in eventQueue)
        {
            action.Invoke();
        }

        eventQueue.Clear();
    }
}