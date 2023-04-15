using KKSpeech;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class OculusVoiceDictationManager : MonoBehaviour
{
    [SerializeField] private Button _recordBtn;
    [SerializeField] private TextMeshProUGUI _recordBtnTxt;
    [SerializeField] private TextMeshProUGUI _resultTxt;

    public void OnFinalResult(string result)
    {
        _recordBtnTxt.text = "Start Recording";
        _resultTxt.text = result;
        _recordBtn.enabled = true;
    }

    public void OnPartialResult(string result)
    {
        _resultTxt.text = result;
    }

    private void OnAvailabilityChange(bool available)
    {
        _recordBtn.enabled = available;
        if (!available)
        {
            _resultTxt.text = "Speech Recognition not available";
        }
        else
        {
            _resultTxt.text = "Say something :-)";
        }
    }

    private void OnAuthorizationStatusFetched(AuthorizationStatus status)
    {
        switch (status)
        {
            case AuthorizationStatus.Authorized:
                _recordBtn.enabled = true;
                break;
            default:
                _recordBtn.enabled = false;
                _resultTxt.text = "Cannot use Speech Recognition, authorization status is " + status;
                break;
        }
    }

    private void OnEndOfSpeech()
    {
        _recordBtnTxt.text = "Start Recording";
    }

    private void OnError(string error)
    {
        Debug.LogError(error);
        _recordBtnTxt.text = "Start Recording";
        _recordBtn.enabled = true;
    }

    private void OnRecordingPressed()
    {
        if (SpeechRecognizer.IsRecording())
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                SpeechRecognizer.StopIfRecording();
                _recordBtnTxt.text = "Start Recording";
            }
        }
        else
        {
            SpeechRecognizer.StartRecording(true);
            _recordBtnTxt.text = "Stop Recording";
            _resultTxt.text = "Say something :-)";
        }
    }
}
