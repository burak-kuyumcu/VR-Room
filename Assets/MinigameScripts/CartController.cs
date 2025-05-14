using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // E�er saya� g�stereceksen

public class CartController : MonoBehaviour
{
    public int cargoCount = 0;
    public TMP_Text statusText;

    // 1?? G�rev y�neticisi referans�
    public QuestManager questManager;

    public void AddCargo()
    {
        cargoCount++;
        if (statusText != null)
            statusText.text = $"{cargoCount}/5 paket y�klendi";

        // 2?? 5 paket y�klendi�inde g�revi tamamla
        if (cargoCount >= 5)
        {
            if (questManager != null)
                questManager.CompleteQuest("Paketleri Arabaya Y�kle");
        }
    }
}


