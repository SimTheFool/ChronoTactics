using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class SkillBarUI : UIChildrenGenerator
{
    public CombatControls combatControls = null;

    void Update()
    {
        this.Paint<KeyValuePair<string, UnityAction>>(this.combatControls.Actions, (uI, action) => {
            uI.GetComponentInChildren<Text>().text = action.Key;
            uI.GetComponent<Button>().onClick.RemoveAllListeners();
            uI.GetComponent<Button>().onClick.AddListener(action.Value);
        });
    }
}
