using UnityEngine;

public class Actor: MonoBehaviour
{
    [SerializeField]
    private string actorName;

    public string getActorName()
    {
        return this.actorName;
    }

    public void setActorName(string actorName)
    {
        this.actorName = actorName;
    }

    [SerializeField]
    private int speed;

    public int getSpeed()
    {
        return this.speed;
    }

    public void setSpeed(int speed)
    {
        this.speed = speed;
    }


    public void Awake()
    {
        DependencyLocator.getTimelineHandler().registerActor(this);
    }


    public override string ToString()
    {
        return this.actorName;
    }
}