using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SkillUIController : MonoBehaviour
{
    private TextMeshProUGUI skillText = null;
    private TextMeshProUGUI SkillText
    {
        get
        {
            if (this.skillText == null)
                this.skillText = this.GetComponentInChildren<TextMeshProUGUI>();

            return this.skillText;
        }
    }

    private Button skillButton = null;
    private Button SkillButton
    {
        get
        {
            if (this.skillButton == null)
                this.skillButton = this.GetComponent<Button>();

            return this.skillButton;
        }
    }

    public bool Interactable
    {
        get => this.skillButton.interactable;
        set => this.skillButton.interactable = value;
    }


    public void SetSkillName(string skillName)
    {
        this.SkillText.text = skillName;
    }

    public void AddClickListener(Action clickListener)
    {
        this.SkillButton.onClick.AddListener(() => clickListener());
    }

    public void ClearClickListeners()
    {
        this.SkillButton.onClick.RemoveAllListeners();
    }
}