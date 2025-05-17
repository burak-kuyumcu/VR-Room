using System.Collections;
using System.Collections.Generic;
/// LampManager.cs
using UnityEngine;

public class LampManager : MonoBehaviour
{
    [Header("Quest")]
    public QuestManager questManager;
    [Tooltip("G�rev ba�l���")]
    public string questTitle = "Lamba Yerle�tir";

    /// <summary>
    /// Called by LampZoneTrigger when a lamp is placed
    /// </summary>
    public void RegisterLamp()
    {
        questManager.Trigger(questTitle);
    }
}


