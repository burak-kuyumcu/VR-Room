using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    // NPC'nin söyleyeceği cümleleri dizi olarak tutar.
    public string[] sentences;
}
