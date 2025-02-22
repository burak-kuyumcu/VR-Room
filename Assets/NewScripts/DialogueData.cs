using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    // NPC'nin s�yleyece�i c�mleleri dizi olarak tutar.
    public string[] sentences;
}
