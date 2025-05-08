using UnityEngine;
using TMPro;

public class TradeManager : MonoBehaviour
{
    public string neededTag;
    public TMP_Text questionText;
    public TMP_Text feedbackText;

    void Start()
    {
        questionText.text = $"L�tfen '{neededTag.Replace("Good_", "")}' se�in";
        feedbackText.text = "";
    }

    public void OnItemPlaced(GameObject item)
    {
        if (item.CompareTag(neededTag))
            feedbackText.text = "Do�ru se�im!";
        else
            feedbackText.text = "Yanl��, tekrar deneyin.";
    }

    void OnTriggerEnter(Collider other)
    {
        OnItemPlaced(other.gameObject);
    }

}
