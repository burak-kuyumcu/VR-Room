using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OfferingZone : MonoBehaviour
{
    [Tooltip("Kabul edilecek adak tag�i")]
    public string acceptedTag;
    [Tooltip("Adak snap noktas�n�n Transform�u")]
    public Transform spot;
    [Tooltip("OfferingsManager referans�")]
    public OfferingsManager manager;

    private bool used;

    private void OnTriggerEnter(Collider other)
    {
        if (used || !other.CompareTag(acceptedTag)) return;

        // Fiziksel snap
        other.transform.position = spot.position;
        other.transform.rotation = spot.rotation;

        // Grab kapat
        var grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null) grab.enabled = false;

        manager.RegisterOffering();
        used = true;
    }
}

