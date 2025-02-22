using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class NoteInteraction : MonoBehaviour
{
    [Header("Not UI Ayarlar�")]
    [Tooltip("Not i�eri�ini g�sterecek UI Paneli (Canvas alt�nda olabilir).")]
    public GameObject noteUIPanel;

    [Tooltip("Notun metnini g�sterecek Text bile�eni.")]
    public Text noteUIText;

    [Tooltip("G�sterilecek not metni.")]
    [TextArea]
    public string noteText = "Buraya not i�eri�i gelecek...";

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        // XR Grab Interactable bile�enini al�yoruz
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("Bu objede XRGrabInteractable bile�eni bulunmuyor!");
        }

        // E�er UI Paneli tan�ml� de�ilse, hata mesaj� verelim.
        if (noteUIPanel == null)
        {
            Debug.LogError("Note UI Paneli atanmam��!");
        }
    }

    private void OnEnable()
    {
        // Objenin al�nd��� (grab) ve b�rak�ld��� (release) olaylara abone oluyoruz
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDisable()
    {
        // Abonelikleri kald�r�yoruz
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    // Notu ald���n�zda tetiklenecek metot
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Notu elinize ald���n�zda, UI panelini a��p not i�eri�ini g�steriyoruz
        if (noteUIPanel != null)
        {
            noteUIPanel.SetActive(true);
            if (noteUIText != null)
            {
                noteUIText.text = noteText;
            }
        }
    }

    // Notu b�rakt���n�zda tetiklenecek metot
    private void OnReleased(SelectExitEventArgs args)
    {
        // Not b�rak�ld���nda, UI panelini kapat�yoruz
        if (noteUIPanel != null)
        {
            noteUIPanel.SetActive(false);
        }
    }
}
