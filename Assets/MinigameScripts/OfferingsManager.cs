using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferingsManager : MonoBehaviour
{
    [Header("Quest")]
    public QuestManager questManager;
    [Tooltip("G�rev ba�l���")]
    public string questTitle = "Sunakta Adaklar";

    public void RegisterOffering()
    {
        questManager.Trigger(questTitle);
    }
}

