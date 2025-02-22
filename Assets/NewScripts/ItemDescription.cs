using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // TextMeshPro kullan�yorsan�z

public class ItemDescription : MonoBehaviour
{
    [Header("UI Ayarlar�")]
    public GameObject descriptionPanel;         // A��klama paneli referans�
    public TextMeshProUGUI descriptionText;       // A��klama metin bile�eni (veya UnityEngine.UI.Text)
    public string description = "Bu itemin a��klamas�."; // A��klama metniniz
    public float displayDuration = 5f;            // Panelin a��k kalaca�� s�re

    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        // Item �zerindeki XR Grab Interactable bile�enini al�yoruz.
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            // Item al�nd���nda �al��acak olaya abone oluyoruz.
            grabInteractable.selectEntered.AddListener(OnItemGrabbed);
        }
    }

    // Item yakaland���nda �a�r�lan metot
    void OnItemGrabbed(SelectEnterEventArgs args)
    {
        // A��klama panelini aktif hale getir
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(true);
        }

        // A��klama metnini g�ncelle
        if (descriptionText != null)
        {
            descriptionText.text = description;
        }

        // Belirli bir s�re sonra paneli kapat
        StartCoroutine(HideDescriptionAfterDelay());
    }

    // Paneli displayDuration s�resi kadar g�sterip sonra kapat�r
    IEnumerator HideDescriptionAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(false);
        }
    }
}

