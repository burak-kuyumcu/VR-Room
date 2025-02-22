using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshPro kullan�yorsan

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    // Diyalog penceresi (Canvas �zerindeki Panel)
    public GameObject dialoguePanel;
    // Diyalog metin alan� (TextMeshProUGUI veya UnityEngine.UI.Text)
    public TextMeshProUGUI dialogueText;

    private Queue<string> sentences;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        sentences = new Queue<string>();

        // Diyalog panelini ba�lang��ta kapat�yoruz.
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    // Diyalo�u ba�latan metot.
    public void StartDialogue(DialogueData dialogueData)
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        sentences.Clear();

        foreach (string sentence in dialogueData.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    // Bir sonraki c�mleyi g�sterir.
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        if (dialogueText != null)
            dialogueText.text = sentence;
    }

    // Diyalo�u sonland�r�r.
    public void EndDialogue()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }
}
