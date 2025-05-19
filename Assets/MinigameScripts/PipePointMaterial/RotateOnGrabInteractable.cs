using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class RotateOnSelect : MonoBehaviour
{
    XRGrabInteractable _grab;

    void Awake()
    {
        _grab = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        // E�er �u anda grab tu�una bas�l� ve obje se�ili ise
        if (_grab.isSelected)
        {
            // En eski (ilk) interactor'u al
            var interactor = _grab.GetOldestInteractorSelecting();
            if (interactor != null)
            {
                // Interactor'�n Y a��s�n� al�p objeyi d�nd�r
                float yAngle = interactor.transform.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0f, yAngle, 0f);
            }
        }
    }
}



