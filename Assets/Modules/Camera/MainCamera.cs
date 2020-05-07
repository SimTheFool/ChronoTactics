using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private static Camera instance = null;
    public static Camera Instance => instance;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.GetComponent<Camera>());
            return;
        }

        if(instance == null)
        {
            instance = this.GetComponent<Camera>();
            return;
        }
    }    
}