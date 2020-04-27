using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class CombatControls : MonoBehaviour
{
    private List<KeyValuePair<string, UnityAction>> actions;
    public List<KeyValuePair<string, UnityAction>> Actions
    {
        get
        {
            return this.actions;
        }
    }

    public void SetActions(List<KeyValuePair<string, UnityAction>> actions)
    {
        this.actions = actions;
    }

    public void EnableCombatControls()
    {
        this.gameObject.SetActive(true);
    }

    public void DisableCombatControls()
    {
        this.gameObject.SetActive(false);
    }
}