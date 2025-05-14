using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuestLogUI : MonoBehaviour
{
    public RectTransform contentParent;
    public GameObject questItemPrefab;
    public QuestLog questLog;

    void OnEnable()
    {
        PopulateQuests();
    }

    public void PopulateQuests()
    {
        // Mevcut ��eleri temizle
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        // Yeni ��eleri ekle
        foreach (var entry in questLog.quests)
        {
            // worldPositionStays = false => local konum/layout�u koru
            var go = Instantiate(questItemPrefab, contentParent, false);
            go.GetComponent<QuestItem>().Setup(entry);
        }

        // Bir frame gecikmeli layout rebuild
        StartCoroutine(DelayedRebuild());
    }

    private IEnumerator DelayedRebuild()
    {
        yield return null; // bir frame bekle
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentParent);
    }
}


