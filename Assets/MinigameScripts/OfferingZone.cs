using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OfferingZone : MonoBehaviour
{
    [Tooltip("Bu b�lgeye kabul edilen tag")]
    public string acceptedTag;
    private bool used = false;

    void OnTriggerEnter(Collider other)
    {
        // Zaten yerle�tirilmi�se ��k
        if (used) return;

        // Do�ru t�rde adak m�?
        if (other.CompareTag(acceptedTag))
        {
            // Adak objesini zona konumuna ta��
            other.transform.position = transform.position;
            // Kavrayabilmeyi kapat
            var grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null) grab.enabled = false;
            used = true;
        }
    }
}

