using System;
using OpenAi.Unity.V1;
using UnityEngine;
using UnityEngine.Networking;

public class OpenAiChatManager : MonoBehaviour
{
    public void GetResponse(string t, Action<string> onResponse, Action<UnityWebRequest> onError)
    {
        if (string.IsNullOrEmpty(t))
        {
            Debug.LogError("Example requires input in input field");
            return;
        }

        OpenAiChatCompleterV1.Instance.Complete(
            t,
            onResponse.Invoke,
            onError.Invoke
        );
    }
}
