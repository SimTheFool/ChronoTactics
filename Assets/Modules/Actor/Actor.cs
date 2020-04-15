using UnityEngine;

public class Actor: MonoBehaviour
{
    [SerializeField]
    private string actorName;
    public string Name
    {
        get
        {
            return this.actorName;
        }
    }

    private int atb = 0;
    public int Atb
    {
        get
        {
            return this.atb;
        }

        set
        {
            this.atb = value;
        }
    }


    [SerializeField]
    private int speed;
    public int Speed
    {
        get
        {
            return this.speed;
        }
    }

    public override string ToString()
    {
        return this.actorName;
    }
}