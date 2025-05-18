using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LampZoneTrigger : MonoBehaviour
{
    [Header("Snap Settings")]
    [Tooltip("Gaz lambas�n�n tam oturaca�� nokta")]
    public Transform lampSpot;

    [Header("Quest")]
    [Tooltip("LampManager component�ini ta��yan obje")]
    public LampManager manager;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("GasLamp")) return;

        other.transform.position = lampSpot.position;
        other.transform.rotation = lampSpot.rotation;

        var grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null) grab.enabled = false;
        var rb = other.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        triggered = true;
        manager.RegisterLamp();
    }
}




