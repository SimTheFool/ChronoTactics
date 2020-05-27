using UnityEngine;

public class ActorStats : BaseActor
{

    [SerializeField]
    private int maxHealth = 100;
    public int MaxHealth
    {
        get => this.maxHealth;
        set
        {
            this.maxHealth = value;
        }
    }

    [SerializeField]
    private int health = 100;
    public int Health
    {
        get => this.health;
        set
        {
            int newHealth = Mathf.Clamp(value, 0, this.maxHealth);
            if(newHealth != this.health)
                this.Events.OnStatsChange(this.Facade);

            this.health = newHealth;
        }
    }

    [SerializeField]
    private int atb = 0;
    public int Atb
    {
        get => this.atb;
        set => this.atb = value;
    }

    [SerializeField]
    private int speed = 100;
    public int Speed => this.speed;
}