using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnManager : MonoBehaviour
{
    [Header("Quest")]
    public QuestManager questManager;
    public string questTitle = "S�tun Birle�tirme";
    [Header("Par�a Say�s�")]
    public int requiredCount = 3;

    private int currentCount = 0;

    void Start()
    {
        // G�rev 0/3 olarak ba�las�n
     
    }

    // SnapZone'lardan �a�r�lacak
    public void OnPartSnapped()
    {
        currentCount++;
        // Her par�a tak�ld���nda questManager.Trigger ile 1 art��
        questManager.Trigger(questTitle);

        if (currentCount >= requiredCount)
        {
            Debug.Log("S�tun tamamland�!");
            // E�er dilerseniz burada ekstra bir �ey de yapabilirsiniz
        }
    }
}

