/// TradeManager.cs
using UnityEngine;
using TMPro;

public class TradeManager : MonoBehaviour
{
    [Header("Quest")]
    public QuestManager questManager;
    public string questTitle = "T�ccar G�revi";
    [Tooltip("Do�ru item tag�i")]
    public string neededTag;
    public TMP_Text questionText;
    public TMP_Text feedbackText;

    private bool started;

    private void Start()
    {
        questionText.text = $"L�tfen '{neededTag.Replace("Good_", "")} ' se�in";
        feedbackText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (started || !other.CompareTag(neededTag)) return;

        questManager.Trigger(questTitle);
        feedbackText.text = "Do�ru se�im!";
        started = true;
    }
}
