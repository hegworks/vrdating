using Meta.WitAi.Dictation.Data;
using Meta.WitAi.Json;
using Oculus.Voice.Dictation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OculusVoiceDictationManager : MonoBehaviour
{
    [SerializeField] private AppDictationExperience _appDictationExperience;

    [SerializeField] private Button _recordBtn;
    [SerializeField] private TextMeshProUGUI _recordBtnTxt;
    [SerializeField] private TextMeshProUGUI _resultTxt;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _appDictationExperience.Activate();
        }
    }

    public void OnStart()
    {
        Debug.Log("OnStart");
    }

    public void OnDictationSessionStarted(DictationSession ds)
    {
        Debug.Log($"OnDictationStart - response: {ds.response}");
    }

    public void OnDictationSessionStopped(DictationSession ds)
    {
        Debug.Log($"OnDictationStopped - response: {ds.response}");
    }

    public void OnPartialTranscription(string transcription)
    {
        Debug.Log($"OnPartialTranscription - transcription: {transcription}");
    }

    public void OnFullTranscription(string transcription)
    {
        Debug.Log($"OnFullTranscription - transcription: {transcription}");
    }

    public void OnStopped()
    {
        Debug.Log("OnStopped");
    }

    public void OnResponse(WitResponseNode wrn)
    {
        Debug.Log($"OnResponse - WitResponseNode: {wrn}");
    }
}
