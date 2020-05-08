using UnityEngine;

public class ActorStats : MonoBehaviour
{
    private Actor actor = null;
    private Actor Actor
    {
        get
        {
            if(this.actor == null)
                this.actor.GetComponent<Actor>();
            return this.actor;
        }
    }

    [SerializeField]
    private int maxHealth = 100;
    public int MaxHealth => this.maxHealth;

    [SerializeField]
    private int health = 100;
    public int Health
    {
        get => this.health;
        set => this.health = Mathf.Clamp(value, 0, this.maxHealth);
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