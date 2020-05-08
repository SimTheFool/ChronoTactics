using UnityEngine;

public class ActorEvents : MonoBehaviour
{
    private delegate void ActorAction(Actor actor);
    private static event ActorAction onPlayableStateActivation;
    public static void OnPlayableStateActivation(Actor actor) => onPlayableStateActivation?.Invoke(actor);

    private delegate void IntAction(int value);
    private event IntAction onHealthChange;
    public void OnHealthChange(int health) => this.onHealthChange?.Invoke(health);
    private event IntAction onMaxHealthChange;
    public void OnMaxHealthChange(int maxHealth) => this.onMaxHealthChange?.Invoke(maxHealth);


}