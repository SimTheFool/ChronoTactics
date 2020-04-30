using UnityEngine;
using UnityEngine.UI;

public class SkillBarUI : UIChildrenGenerator
{
    public CombatControls combatControls = null;

    void Update()
    {
        if(this.combatControls.Actor == null) return;

        this.Paint<Skill>(this.combatControls.Actor.Skills, (uI, skill) => {
            uI.GetComponentInChildren<Text>().text = skill.Name;
            uI.GetComponent<Button>().onClick.RemoveAllListeners();
            uI.GetComponent<Button>().onClick.AddListener(() => {
                this.combatControls.ClickListener(skill);
            });
        });
    }
}
