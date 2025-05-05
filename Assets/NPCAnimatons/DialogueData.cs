using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue Data", fileName = "New Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [Tooltip("NPC�nin diyalog ba�l��� olarak g�r�nen ad�")]
    public string npcName;

    [Tooltip("Diyalog s�ras�nda okunacak c�mleler")]
    public string[] sentences;
    // -- veya --
    // public List<string> sentences;
}
