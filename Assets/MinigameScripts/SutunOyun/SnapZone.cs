using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapZone : MonoBehaviour
{
    [Tooltip("Beklenen tag: Column_Base, Column_Mid veya Column_Capital")]
    public string expectedTag;

    [Tooltip("ColumnManager component�i (�l�e�i 1,1,1)")]
    public ColumnManager columnManager;

    [Tooltip("E�er bo� b�rak�l�rsa ColumnManager��n alt�na ba�lan�r")]
    public Transform snapParentOverride;

    private bool occupied = false;

    private void OnTriggerEnter(Collider other)
    {
        TrySnap(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TrySnap(other);
    }

    private void TrySnap(Collider other)
    {
        if (occupied) return;
        if (!other.CompareTag(expectedTag)) return;

        // 1) Hangi parent'a ba�layaca��z?
        Transform snapParent = snapParentOverride != null
            ? snapParentOverride
            : (columnManager != null ? columnManager.transform : null);

        // 2) World-space pozisyonu koruyarak parent�a ta��
        if (snapParent != null)
            other.transform.SetParent(snapParent, worldPositionStays: true);

        // 3) Par�ay� tam olarak SnapZone'un pozisyonuna hizala
        other.transform.position = transform.position;
        other.transform.rotation = transform.rotation;

        // 4) Rigidbody�yi al ve kinematik yap, hareketi kilitle
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        // 5) Collider�� kapat
        Collider col = other.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        // 6) Grab etkile�imini pasifle�tir
        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            // �Bo�� interaction layer i�in s�f�r mask
            grab.interactionLayers = InteractionLayerMask.GetMask();
        }

        occupied = true;
        Debug.Log($"{expectedTag} ba�ar�yla yerle�ti ve sabitlendi!");

        // 7) ColumnManager�a haber ver
        if (columnManager != null)
            columnManager.OnPartSnapped();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        BoxCollider bc = GetComponent<BoxCollider>();
        if (bc != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position + bc.center, bc.size);
        }
    }
#endif
}


