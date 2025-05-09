using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest System/Quest Entry")]
public class QuestEntry : ScriptableObject
{
    public string title;            // �rn. "T�ccar�n Tezgah�n� Bul"
    [TextArea] public string description;  // �rn. " do�ru �r�n� se�ip tezgaha b�rak"
    [HideInInspector] public bool isCompleted = false;
}
