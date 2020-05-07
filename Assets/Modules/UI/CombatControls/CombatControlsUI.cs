using UnityEngine;
using UnityEngine.Events;

public class CombatControlsUI : MonoBehaviour
{
    private Actor actor;
    public Actor Actor
    {
        get
        {
            return this.actor;
        }
    }

    private UnityAction<Skill> clickListener = null;
    public UnityAction<Skill> ClickListener
    {
        get
        {
            return this.clickListener;
        }
    }

    public void SetClickListener(UnityAction<Skill> clickListener)
    {
        this.clickListener = clickListener;
    }

    public void SetActor(Actor actor)
    {
        this.actor = actor;
    }

    public void Enable()
    {
        this.gameObject.SetActive(true);
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}