using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text descText;

    public void Setup(QuestEntry entry)
    {
        titleText.text = entry.title;
        descText.text = entry.description;
        // İleride isCompleted'e göre renk değiştirebilirsiniz
    }
}
