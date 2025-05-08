// Assets/Scripts/NPCVision.cs
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerLooking))]
[RequireComponent(typeof(Animator))]
public class NPCVision : MonoBehaviour
{
    [Header("G�r�� Ayarlar�")]
    [Tooltip("Ka� birim uzaktaki objeleri g�rebilsin (mesafe s�n�r�)")]
    public float viewRadius = 20f;
    [Tooltip("A�� kontrol�n� atlamak istersen i�aretle (yaln�zca mesafe bazl� kontrol)")]
    public bool ignoreAngle = false;
    [Range(0, 360)]
    [Tooltip("Nesne ile NPC �n vekt�r� aras�ndaki maksimum a�� (derece)")]
    public float viewAngle = 120f;
    [Tooltip("Raycast'in hangi katmanlardaki nesnelere �arpaca��n� se�in (NPC layer�� hari�)")]
    public LayerMask obstructionMask = ~0;

    [Header("Ba�l� Bile�enler")]
    [Tooltip("NPC'nin d�nmesini sa�layan PlayerLooking bile�eni")]
    public PlayerLooking lookingScript;
    [Tooltip("NPC Animator bile�eni, animasyon tetikleyicileri i�in")]
    public Animator animator;

    [Header("Animasyon Triggers")]
    [Tooltip("Player alg�land���nda tetiklenecek Trigger")] public string headNodTrigger = "HeadNod";
    [Tooltip("G�r��ten ��kt���nda tetiklenecek Trigger")] public string idleTrigger = "Idle";

    private Transform cameraT;
    private int npcLayer;
    private bool playerInSight = false;

    void Start()
    {
        // Kamera referans�
        cameraT = Camera.main?.transform;
        if (cameraT == null)
            throw new Exception("NPCVision: MainCamera bulunamad�! Tag ve sahne hiyerar�isini kontrol edin.");

        // PlayerLooking atamas�
        lookingScript = lookingScript ?? GetComponent<PlayerLooking>();
        if (lookingScript == null)
            Debug.LogError("NPCVision: PlayerLooking component bulunamad�!");
        lookingScript.enabled = false;

        // Animator atamas�
        animator = animator ?? GetComponent<Animator>();
        if (animator == null)
            Debug.LogWarning("NPCVision: Animator component yok, animasyon tetiklenemeyecek.");

        // NPC layer'�n� obstructionMask'tan ��kar
        npcLayer = gameObject.layer;
        obstructionMask &= ~(1 << npcLayer);
    }

    void Update()
    {
        Vector3 dir = cameraT.position - transform.position;
        float dist = dir.magnitude;
        Vector3 flat = new Vector3(dir.x, 0f, dir.z).normalized;
        float ang = Vector3.Angle(transform.forward, flat);

        // Debug g�rselle�tirmeleri
        Debug.DrawLine(transform.position, transform.position + transform.forward * viewRadius, Color.red);
        Debug.DrawLine(transform.position, cameraT.position, Color.green);
        Debug.Log($"NPCVision: dist={dist:F2}, ang={ang:F1}");

        // Menzil ve a�� kontrol�
        bool inRange = dist <= viewRadius;
        bool inAngle = ignoreAngle || ang <= viewAngle * 0.5f;

        if (inRange && inAngle)
        {
            Vector3 eyePos = transform.position + Vector3.up * 1.5f + transform.forward * 0.1f;
            bool blocked = Physics.Linecast(eyePos, cameraT.position, obstructionMask);
            Debug.Log($"  Raycast blocked={blocked}");

            if (!blocked)
            {
                if (!playerInSight)
                {
                    playerInSight = true;
                    lookingScript.enabled = true;
                    if (animator != null)
                        animator.SetTrigger(headNodTrigger);
                    Debug.Log(">>> Player g�r�ld�, PlayerLooking ve HeadNod animasyonu aktifle�tirildi.");
                }
                // alg�land�ktan sonra devam eden kodu engellemek i�in return ekleyebilirsiniz
                return;
            }
            else
            {
                Debug.Log("Ray, arada bir engele �arpt�.");
            }
        }
        else if (playerInSight)
        {
            playerInSight = false;
            lookingScript.enabled = false;
            if (animator != null)
                animator.SetTrigger(idleTrigger);
            Debug.Log("--- Player g�r��ten ��kt�, PlayerLooking ve Idle animasyonu pasif edildi.");
        }
    }
}