using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "ScriptableTest", menuName = "ChronoTactics/ScriptableTest", order = 0)]
public class ScriptableTest : ScriptableObject, ISerializationCallbackReceiver
{
    /* public string testString = "";

    [SerializeField]
    byte[] serializedFunc;
    public Func<string, string> testFunc; */

    public void OnBeforeSerialize()
    {
        /* if(testFunc != null)
            this.serializedFunc = ByteSerializer.Serialize(this.testFunc);

        Debug.Log("serialize"); */
    }

    public void OnAfterDeserialize()
    {
        /* Debug.Log("deserialize");

        if(this.serializedFunc != null && this.serializedFunc.Length > 0)
            this.testFunc = ByteSerializer.Deserialize<Func<string, string>>(this.serializedFunc); */
    }
}