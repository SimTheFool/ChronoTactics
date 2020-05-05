using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{

    private TimelineController timelineController = null;
    private Text timerLabel = null;

    void Start()
    {
        this.timelineController = DependencyLocator.getTimelineController();
        this.timerLabel = this.GetComponent<Text>();
    }

    void Update()
    {
        this.timerLabel.text = $"{this.timelineController.Timer.ToString("#.00")} sec";
    }
}
