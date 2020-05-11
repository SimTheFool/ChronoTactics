using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AgentUIController : MonoBehaviour
{
    private TextMeshProUGUI agentText = null;
    private TextMeshProUGUI AgentText
    {
        get
        {
            if (this.agentText == null)
                this.agentText = this.GetComponent<TextMeshProUGUI>();

            return this.agentText;
        }
    }

    public void SetAgentName(string agentName)
    {
        this.AgentText.text = agentName;
    }
}