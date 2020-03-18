using UnityEngine;

public class DependencyLocator : MonoBehaviour
{

    private static DependencyLocator instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public static TimelineHandler getTimelineHandler()
    {
        return instance.gameObject.GetComponent<TimelineHandler>();
    }
}
