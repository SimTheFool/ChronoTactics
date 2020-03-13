public struct TimelineActor
{
    public Actor gameActor;
    public int atb;

    public TimelineActor(Actor gameActor, int atb = 0)
    {
        this.gameActor = gameActor;
        this.atb = atb;
    }

    public override string ToString()
    {
        return this.gameActor.ToString();
    }
}