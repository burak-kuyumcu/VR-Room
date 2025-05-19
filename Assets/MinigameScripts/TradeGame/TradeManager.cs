using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class TradeManager : MonoBehaviour
{
    [Header("Quest & UI")]
    public QuestManager questManager;
    public string questTitle = "T�ccar G�revi";

    [Header("Trade UI Prefab & Container")]
    public GameObject tradeCanvasPrefab;
    public Transform tradeUIContainer;

    [Header("Trade UI Elements (Prefab i�indeki Binder�dan alaca��z)")]
    private TradeUIBinder _binder;
    private GameObject _uiInstance;

    [Header("Game Logic")]
    [Tooltip("Teslim edilmesi istenen nesnelerin tag�leri")]
    public string[] neededTags;
    private HashSet<string> _delivered = new HashSet<string>();

    void Awake()
    {
        // Container�� temizle (edit�rden drag-drop kalan vs)
        foreach (Transform c in tradeUIContainer) Destroy(c.gameObject);

        // Canvas�� clone�la, parent�la, local pos=0
        _uiInstance = Instantiate(tradeCanvasPrefab, tradeUIContainer, false);

        // ��inden binder�� al
        _binder = _uiInstance.GetComponent<TradeUIBinder>();
        if (_binder == null)
            Debug.LogError("TradeUIBinder bulunamad�! Prefab�ta binder ekli mi?");

        // Ba�ta kapal� olsun
        _uiInstance.SetActive(false);
    }

    void Start()
    {
        // Quest�i ba�lat (0/neededTags.Length g�sterir)
       

        // UI�� a��p nelerin istendi�ini yaz
        _uiInstance.SetActive(true);
        _binder.questionText.text = "Teslim etmeniz gerekenler:\n" +
            string.Join(", ", neededTags.Select(t => t.Replace("Good_", "")));
        _binder.feedbackText.text = $"0/{neededTags.Length} teslim edildi.";
    }

    void OnTriggerEnter(Collider other)
    {
        var tag = other.tag;
        // Sadece neededTags i�indekileri kabul et
        if (!neededTags.Contains(tag)) return;

        // Ayn� tag�i birden fazla saymamak i�in kontrol et
        if (_delivered.Add(tag))
        {
            // QuestManager�� �a��r (progres += 1)
            questManager.Trigger(questTitle);

            // UI�� g�ncelle
            int done = _delivered.Count;
            int total = neededTags.Length;
            _binder.questionText.text = $"{done}/{total} teslim edildi.";
            _binder.feedbackText.text = $"{tag.Replace("Good_", "")} teslim edildi!";

            // E�er hepsi bitti ise tebrik et
            if (done == total)
            {
                _binder.feedbackText.text += "\nT�m e�yalar teslim edildi!";
                Invoke(nameof(HideUI), 3f);
            }
        }
    }

    void HideUI()
    {
        _uiInstance.SetActive(false);
    }
}

