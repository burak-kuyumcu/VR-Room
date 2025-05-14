using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OfferingZone : MonoBehaviour
{
    [Tooltip("Bu b�lgeye kabul edilen tag")]
    public string acceptedTag;

    [Tooltip("Adak objesinin yerle�tirilece�i nokta")]
    public Transform spot;

    [Tooltip("G�rev y�neticisi")]
    public QuestManager questManager;

    [Tooltip("Tamamlama �a�r�s� i�in QuestEntry.title")]
    public string questTitle;

    private bool used = false;

    void OnTriggerEnter(Collider other)
    {
        // Zaten bir adak yerle�tirilmi�se ��k
        if (used) return;

        // Do�ru t�rde adak m�?
        if (other.CompareTag(acceptedTag))
        {
            // 1) Objeyi 'spot' noktas�na ta�� ve rotasyonu e�itle
            other.transform.position = spot.position;
            other.transform.rotation = spot.rotation;

            // 2) Bir daha kavranamas�n
            var grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null) grab.enabled = false;

            used = true;

            // 3) G�revi tamamla (QuestManager ile)
            if (questManager != null && !string.IsNullOrEmpty(questTitle))
            {
                questManager.CompleteQuest(questTitle);
            }
        }
    }
}

