using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerlooking : MonoBehaviour
{
    [Tooltip("VR Rig i�indeki Kamera (Main Camera) atanmal� ve otomatik bulunabilir.")]
    public Transform cameraTransform;
    [Tooltip("D�n�� h�z�")] public float turnSpeed = 2f;

    void Start()
    {
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (cameraTransform == null) return;
        // Yaln�zca yatay d�zlemde d�n
        Vector3 direction = cameraTransform.position - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion target = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                target,
                Time.deltaTime * turnSpeed);
        }
    }
}
