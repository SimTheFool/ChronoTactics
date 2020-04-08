using UnityEngine;

public class DebugPathfindingComponent : MonoBehaviour
{
    public delegate void DrawingGizmosEvent();
    public DrawingGizmosEvent OnDrawingGizmos = delegate {};

    public void OnDrawGizmos()
    {
        OnDrawingGizmos.Invoke();
    }
}