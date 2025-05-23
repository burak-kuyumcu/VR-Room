// ArcheryManager.cs
using UnityEngine;

public class ArcheryManager : MonoBehaviour
{
    [Header("Quest Integration")]
    public QuestManager questManager;
    public string questTitle = "Ok�uluk G�revi";
    public int requiredHits = 5;

    private int _currentHits = 0;

    void Start()
    {
        _currentHits = 0;
        Debug.Log($"[ArcheryManager] Ba�lad�: 0/{requiredHits}");
    }

    /// <summary>
    /// Bir hedef vuruldu�unda �a�r�l�r.
    /// </summary>
    public void RegisterHit()
    {
        Debug.Log($"[ArcheryManager] RegisterHit �a�r�ld�, �nceki:{_currentHits}");
        if (_currentHits >= requiredHits)
        {
            Debug.Log("[ArcheryManager] Zaten tamamland�, return.");
            return;
        }

        _currentHits++;
        Debug.Log($"[ArcheryManager] �lerleme: {_currentHits}/{requiredHits}");

        // QuestManager'� her ad�mda tetikle
        questManager.Trigger(questTitle);

        if (_currentHits == requiredHits)
            Debug.Log("[ArcheryManager] T�m hedefler vuruldu!");
    }
}




