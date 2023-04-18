using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private TextMeshProUGUI _btnTxt;
    [SerializeField] private TextMeshProUGUI _resultTxt;

    private string _lastQuestion;

    public void StartedListening()
    {
        _btnTxt.text = "Listening...";
    }

    public void StoppedListening()
    {
        _btnTxt.text = "Start Listening";
    }

    public void SetUnfinishedDictationTo(string s)
    {
        _resultTxt.text = $"- {s}...";
    }

    public void SetFinishedDictationTo(string s)
    {
        _resultTxt.text = $"- {s}\n\n" +
                          $"Generating Response...";
        _lastQuestion = $"- {s}";
    }

    public void SetFinishedResponseTo(string s)
    {
        _resultTxt.text = _lastQuestion + "\n\n" +
                          $"+ {s}";
    }
}
