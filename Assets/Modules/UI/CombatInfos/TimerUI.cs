using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{

    private TimelineHandler timelineHandler = null;
    private Text timerLabel = null;

    void Start()
    {
        this.timelineHandler = DependencyLocator.getTimelineHandler();
        this.timerLabel = this.GetComponent<Text>();
    }

    void Update()
    {
        this.timerLabel.text = $"{this.timelineHandler.Timer.ToString("#.00")} sec";
    }
}
