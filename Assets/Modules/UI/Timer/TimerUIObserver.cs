using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUIObserver : MonoBehaviour
{

    private TimelineController timelineController = null;
    private TextMeshProUGUI timerLabel = null;

    void Start()
    {
        this.timelineController = DependencyLocator.getTimelineController();
        this.timerLabel = this.GetComponent<TextMeshProUGUI>();

        this.timelineController.onTimerChange += this.RenderTimer;
    }

    void RenderTimer(float time)
    {
        this.timerLabel.text = $"{time.ToString("#.00")} sec";
    }
}
