using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [Header("NPC Bilgisi")]
    public string npcName;              // Inspector'da g�r�n�r

    [Header("Diyalog C�mleleri")]
    [TextArea(2, 5)]
    public string[] sentences;          // Inspector'da dizi olarak g�z�k�r
}
