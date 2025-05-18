using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class RotateOnSelect : MonoBehaviour
{
    XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        // E�er �u anda grab tu�una bas�l� ve bu interactor ile se�ildiyse
        if (grab.isSelected && grab.selectingInteractor != null)
        {
            // Interactor��n Y a��s�n� al
            float yAngle = grab.selectingInteractor.transform.eulerAngles.y;
            // Sadece Y ekseninde d�n
            transform.rotation = Quaternion.Euler(0f, yAngle, 0f);
        }
    }
}


