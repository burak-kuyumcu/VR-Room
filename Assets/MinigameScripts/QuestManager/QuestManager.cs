using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    public QuestLog questLog;
    public QuestLogUI questLogUI;

    /// <summary>
    /// Ba�l��� verilen g�revi tamamland� olarak i�aretler ve UI�� yeniler.
    /// </summary>
    public void CompleteQuest(string title)
    {
        // Ba�l��a g�re QuestEntry bul
        var entry = questLog.quests.FirstOrDefault(q => q.title == title);
        if (entry == null)
        {
            Debug.LogWarning($"Quest '{title}' bulunamad�!");
            return;
        }

        // Tamamland� olarak i�aretle
        entry.isCompleted = true;

        // UI�� g�ncelle
        questLogUI.PopulateQuests();
    }
}
