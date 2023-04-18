using System;
using Meta.WitAi.Dictation.Data;
using Meta.WitAi.Json;
using Oculus.Voice.Dictation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OculusVoiceDictationManager : MonoBehaviour
{
    [SerializeField] private AppDictationExperience _appDictationExperience;

    public event Action OnStartedListening;
    public event Action<string> OnPartiallyTranscribed;
    public event Action<string> OnFinallyTranscribed;
    public event Action OnStoppedListening;

    private void OnEnable()
    {
        // _appDictationExperience.AudioEvents.OnMicStartedListening.AddListener(OnStart);
        // _appDictationExperience.AudioEvents.OnMicStoppedListening.AddListener(OnStopped);
        // _appDictationExperience.TranscriptionEvents.OnPartialTranscription.AddListener(OnPartialTranscription);
        // _appDictationExperience.TranscriptionEvents.OnFullTranscription.AddListener(OnFullTranscription);

        _appDictationExperience.DictationEvents.onDictationSessionStarted.AddListener(OnDictationSessionStarted);
        _appDictationExperience.DictationEvents.onDictationSessionStopped.AddListener(OnDictationSessionStopped);
        _appDictationExperience.DictationEvents.onStart.AddListener(OnStart);
        _appDictationExperience.DictationEvents.onStopped.AddListener(OnStopped);
        _appDictationExperience.DictationEvents.onPartialTranscription.AddListener(OnPartialTranscription);
        _appDictationExperience.DictationEvents.onFullTranscription.AddListener(OnFullTranscription);
    }

    private void OnDisable()
    {
        _appDictationExperience.DictationEvents.onDictationSessionStarted.RemoveListener(OnDictationSessionStarted);
        _appDictationExperience.DictationEvents.onDictationSessionStopped.RemoveListener(OnDictationSessionStopped);
        _appDictationExperience.DictationEvents.onStart.RemoveListener(OnStart);
        _appDictationExperience.DictationEvents.onStopped.RemoveListener(OnStopped);
        _appDictationExperience.DictationEvents.onPartialTranscription.RemoveListener(OnPartialTranscription);
        _appDictationExperience.DictationEvents.onFullTranscription.RemoveListener(OnFullTranscription);
    }

    #region AppDictationExperience Events
    public void OnStart()
    {
        OnStartedListening?.Invoke();
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
        OnPartiallyTranscribed?.Invoke(transcription);
        Debug.Log($"OnPartialTranscription - transcription: {transcription}");
    }

    public void OnFullTranscription(string transcription)
    {
        OnFinallyTranscribed?.Invoke(transcription);
        Debug.Log($"OnFullTranscription - transcription: {transcription}");
    }

    public void OnStopped()
    {
        OnStoppedListening?.Invoke();
        Debug.Log("OnStopped");
    }

    public void OnResponse(WitResponseNode wrn)
    {
        Debug.Log($"OnResponse - WitResponseNode: {wrn}");
    }
    #endregion

    public void StartListening()
    {
        _appDictationExperience.Activate();
    }

    public void StopListening()
    {
        _appDictationExperience.Deactivate();
    }

    public bool IsListening()
    {
        return _appDictationExperience.Active;
    }
}
