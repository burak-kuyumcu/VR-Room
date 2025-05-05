using UnityEngine;

public class NPCVision : MonoBehaviour
{
    public float viewDistance = 5f;
    [Range(0, 360)] public float viewAngle = 90f;
    [Tooltip("Diyalo�a engel olmayan layer'lar (�rn. Player ve NPC)")]
    public LayerMask detectionMask;
    [Tooltip("Diyalo�u kesebilecek engellerin layer'lar� (�rn: Walls)")]
    public LayerMask obstructionMask;
    public string playerTag = "Player";

    void Update()
    {
        // 1) Menzil testi
        Collider[] hits = Physics.OverlapSphere(transform.position, viewDistance, detectionMask);
        foreach (var hit in hits)
        {
            if (!hit.CompareTag(playerTag)) continue;

            // 2) A��sal test
            Vector3 dir = (hit.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dir) > viewAngle / 2) continue;

            // 3) Engel testi (Linecast)
            RaycastHit info;
            Vector3 origin = transform.position + Vector3.up * 1.5f;
            Vector3 target = hit.transform.position + Vector3.up * 1.0f;
            if (!Physics.Linecast(origin, target, out info, obstructionMask))
            {
                Debug.Log("NPCVision: Sizi g�rd�m!");
                // Ba�ka bir yerde StartDialogue'� buradan da �a��rabilirsiniz
                return;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
        Vector3 left = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward;
        Vector3 right = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + left * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + right * viewDistance);
    }
}
