using UnityEngine;

public class PlayerLooking : MonoBehaviour
{
    [Tooltip("XR Rig i�indeki Kamera Transform'u")]
    public Transform cameraTransform;
    [Tooltip("D�n�� h�z�")]
    public float turnSpeed = 2f;

    void Start()
    {
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        // Yaln�zca yatay d�zlemde d�n
        Vector3 direction = cameraTransform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                turnSpeed * Time.deltaTime
            );
        }
    }
}
