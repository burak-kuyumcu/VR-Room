using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class PipeSegment : MonoBehaviour
{
    public bool isAligned = false;            // D�nd� m� kontrol�
    public float alignThreshold = 1f;         // Angle fark� tolerans�

    XRGrabInteractable grabable;
    Rigidbody rb;

    void Awake()
    {
        grabable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // Grab al�nd���nda 90� d�nd�r
        grabable.selectEntered.AddListener(_ => Rotate90());

        // B�rak�ld���nda f�rlamay� �nle
        grabable.selectExited.AddListener(OnRelease);

        // Throw atmay� kapat (project settings�e de bakabilirsiniz)
        grabable.throwOnDetach = false;
    }

    void Rotate90()
    {
        // Boruyu Y ekseninde d�nd�r�yoruz:
        transform.Rotate(0f, 90f, 0f, Space.World);
        CheckAlignment();
    }


    void CheckAlignment()
    {
        // Y a��s�n� en yak�n 90� kat�na yuvarla
        float y = transform.eulerAngles.y;
        float rounded = Mathf.Round(y / 90f) * 90f;
        isAligned = Mathf.Abs(Mathf.DeltaAngle(y, rounded)) <= alignThreshold;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        // F�rlamay� tamamen durdur
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // E�er do�ru hizadaysa pozisyonu dondur
        if (isAligned)
        {
            // Yaln�zca d�nmesini istersen:
            rb.constraints = RigidbodyConstraints.FreezePositionX
                           | RigidbodyConstraints.FreezePositionY
                           | RigidbodyConstraints.FreezePositionZ
                           | RigidbodyConstraints.FreezeRotationX
                           | RigidbodyConstraints.FreezeRotationZ;
            // Y eksenindeki rotasyonu serbest b�rak 
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        }
    }

    void OnDestroy()
    {
        grabable.selectEntered.RemoveListener(_ => Rotate90());
        grabable.selectExited.RemoveListener(OnRelease);
    }
}


