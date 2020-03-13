public class Actor
{
    public string name;
    public int speed;

    public Actor(string name, int speed = 50)
    {
        this.name = name;
        this.speed = speed;
    }

    public override string ToString()
    {
        return this.name;
    }
}