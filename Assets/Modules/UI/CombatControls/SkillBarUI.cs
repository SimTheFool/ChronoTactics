using UnityEngine;
using UnityEngine.UI;

public class SkillBarUI : UIChildrenGenerator
{
    public CombatControlsUI combatControlsUI = null;

    void Update()
    {
        if(this.combatControlsUI.Actor == null) return;

        this.Paint<Skill>(this.combatControlsUI.Actor.Skills.All, (uI, skill) => {
            uI.GetComponentInChildren<Text>().text = skill.Name;
            uI.GetComponent<Button>().onClick.RemoveAllListeners();
            uI.GetComponent<Button>().onClick.AddListener(() => {
                this.combatControlsUI.ClickListener(skill);
            });
        });
    }
}
