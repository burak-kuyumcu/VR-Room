using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;
    [Tooltip("Diyalog panelini i�eren GameObject")] public GameObject panel;
    [Tooltip("Panel i�indeki TextMeshPro komponenti")] public TMP_Text textBox;

    private System.Action onComplete;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        panel?.SetActive(false);
    }

    /// <summary>
    /// Diyalo�u ba�lat�r ve tamamland���nda callback tetikler
    /// </summary>
    public void Show(string[] lines, System.Action onComplete)
    {
        if (panel == null || textBox == null) return;
        this.onComplete = onComplete;
        panel.SetActive(true);
        StartCoroutine(TypeLines(lines));
    }

    private IEnumerator TypeLines(string[] lines)
    {
        foreach (var line in lines)
        {
            textBox.text = line;
            yield return new WaitForSeconds(2f);
        }
        panel.SetActive(false);
        onComplete?.Invoke();
    }
}