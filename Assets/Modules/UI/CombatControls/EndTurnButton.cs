using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    private Button button = null;
    private TimelineHandler timelineHandler = null;

    void Start()
    {
        this.button = this.GetComponent<Button>();
        this.timelineHandler = DependencyLocator.getTimelineHandler();
        this.button.onClick.AddListener(timelineHandler.EndTurn);
    }
}
