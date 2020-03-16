public struct TimelineActor
{

    // Represent the an actor from the timeline point of view, at a given instant.

    private Actor gameActor;
    private int atb;

    public TimelineActor(Actor gameActor, int atb = 0)
    {
        this.gameActor = gameActor;
        this.atb = atb;
    }

    public int getAtb()
    {
        return this.atb;
    }

    public void setAtb(int atb)
    {
        this.atb = atb;
    }

    public int getSpeed()
    {
        return this.gameActor.getSpeed();
    }

    public override string ToString()
    {
        return this.gameActor.ToString();
    }
}