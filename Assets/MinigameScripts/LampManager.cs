using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampManager : MonoBehaviour
{
    [Header("Quest")]
    public QuestManager questManager;
    [Tooltip("G�rev ba�l���")]
    public string questTitle = "Lamba Yerle�tir";

    public void RegisterLamp()
    {
        questManager.Trigger(questTitle);
    }
}


