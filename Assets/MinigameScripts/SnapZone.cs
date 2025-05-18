using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapZone : MonoBehaviour
{
    [Tooltip("Beklenen tag: Column_Base, Column_Mid veya Column_Capital")]
    public string expectedTag;

    private bool occupied = false;

    private void OnTriggerEnter(Collider other) => TrySnap(other);
    private void OnTriggerStay(Collider other) => TrySnap(other);

    private void TrySnap(Collider other)
    {
        if (occupied) return;
        if (other.CompareTag(expectedTag))
        {
            // Snap i�lemi
            other.transform.SetParent(transform);
            other.transform.localPosition = Vector3.zero;
            other.transform.localRotation = Quaternion.identity;

            // XRGrabInteractable�i devre d��� b�rak
            var grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null) grab.interactionLayerMask = 0;

            occupied = true;
            Debug.Log(expectedTag + " ba�ar�yla yerle�ti!");

            // �ste�e ba�l�: UnityEvent tetikle (GameManager�a ba�lamak i�in)
            // onSnap?.Invoke();
        }
    }

    // [Optional] public UnityEvent onSnap;



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // Sar� ���kl� bir tel kafes �izecek
        Gizmos.color = Color.yellow;
        var col = GetComponent<BoxCollider>();
        if (col != null)
            Gizmos.DrawWireCube(transform.position + col.center, col.size);
    }
#endif

}
