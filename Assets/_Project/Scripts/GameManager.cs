using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private OpenAiChatManager _aiChatManager;
    [SerializeField] private OculusVoiceDictationManager _dictationManager;
    [SerializeField] private UiManager _uiManager;

    private string _transcription;
    private bool _shouldListen;

    private void OnEnable()
    {
        _dictationManager.OnStartedListening += DictationManager_OnStartedListening;
        _dictationManager.OnPartiallyTranscribed += DictationManager_OnPartiallyTranscribed;
        _dictationManager.OnFinallyTranscribed += DictationManager_OnFinallyTranscribed;
        _dictationManager.OnStoppedListening += DictationManager_OnStoppedListening;
    }

    private void OnDisable()
    {
        _dictationManager.OnStartedListening -= DictationManager_OnStartedListening;
        _dictationManager.OnPartiallyTranscribed -= DictationManager_OnPartiallyTranscribed;
        _dictationManager.OnFinallyTranscribed -= DictationManager_OnFinallyTranscribed;
        _dictationManager.OnStoppedListening -= DictationManager_OnStoppedListening;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        _shouldListen = !_shouldListen;

        if (_shouldListen && !_dictationManager.IsListening())
        {
            _dictationManager.StartListening();
        }
        else if (!_shouldListen && _dictationManager.IsListening())
        {
            StopListening();
        }
    }

    private void DictationManager_OnStartedListening()
    {
        _uiManager.StartedListening();
    }

    private void DictationManager_OnPartiallyTranscribed(string t)
    {
        _uiManager.SetUnfinishedDictationTo(_transcription + " " + t);
    }

    private void DictationManager_OnFinallyTranscribed(string t)
    {
        _transcription += " " + t;
        _uiManager.SetUnfinishedDictationTo(_transcription);
    }

    private void DictationManager_OnStoppedListening()
    {
        _uiManager.StoppedListening();
        if (!_shouldListen) return;
        Invoke(nameof(RestartListening), 0.25f);
    }

    private void RestartListening()
    {
        _dictationManager.StartListening();
    }

    private void StopListening()
    {
        _dictationManager.StopListening();
        _uiManager.StoppedListening();
        _uiManager.SetFinishedDictationTo(_transcription);
        _aiChatManager.GetResponse(_transcription,
            r => { _uiManager.SetFinishedResponseTo(r); },
            e => { Debug.LogError($"ai response failed with error: {e}"); }
        );
        _transcription = string.Empty;
    }
}
