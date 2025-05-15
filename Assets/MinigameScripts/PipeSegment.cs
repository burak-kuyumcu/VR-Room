using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class PipeSegment : MonoBehaviour
{
    void Awake()
    {
        var grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(_ => Rotate90());
    }

    void Rotate90()
    {
        // Y ekseni etraf�nda 90� d�nd�r
        transform.Rotate(0, 90, 0, Space.World);
    }
}




