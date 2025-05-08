// Assets/Scripts/DialogueManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Atamalar�")]
    [Tooltip("Diyalog panelinin GameObject'i")]
    public GameObject panelObject;  // Panel GameObject
    [Tooltip("NPC ismini g�steren TextMeshProUGUI")]
    public TMP_Text nameText;
    [Tooltip("Diyalog sat�rlar�n� g�sterecek TextMeshProUGUI")]
    public TMP_Text dialogueText;
    [Tooltip("�leri tu�u i�in buton (Next)")]
    public Button nextButton;

    [Header("Yazma H�z� (saniye)")]
    [Tooltip("Her karakter aras� bekleme s�resi")]
    public float typingSpeed = 0.03f;

    private Queue<string> sentences;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        sentences = new Queue<string>();

        // Next butonuna listener ekle
        if (nextButton != null)
            nextButton.onClick.AddListener(DisplayNextSentence);
    }

    public void StartDialogue(DialogueData data)
    {
        if (data == null || data.sentences == null || data.sentences.Length == 0)
        {
            Debug.LogWarning("DialogueManager: Ba�lat�lacak veri yok!");
            return;
        }

        // Paneli aktif et
        if (panelObject != null)
            panelObject.SetActive(true);

        // NPC ismini ayarla
        if (nameText != null)
            nameText.text = data.npcName;

        // Kuyru�u temizle ve c�mleleri ekle
        sentences.Clear();
        foreach (var s in data.sentences)
            sentences.Enqueue(s);

        // �lk c�mleyi g�ster
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // E�er kuyruk bo�sa diyalog biti�i
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // Bir sonraki c�mle
        string line = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(line));
    }

    private IEnumerator TypeSentence(string line)
    {
        if (dialogueText == null)
            yield break;

        dialogueText.text = string.Empty;
        foreach (char c in line.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void EndDialogue()
    {
        // Paneli deaktif et
        if (panelObject != null)
            panelObject.SetActive(false);

        // Butonu temizle
        if (nextButton != null)
            nextButton.onClick.RemoveListener(DisplayNextSentence);
    }
}
