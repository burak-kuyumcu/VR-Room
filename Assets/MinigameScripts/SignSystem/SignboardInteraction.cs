using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(XRSimpleInteractable))]
public class SignboardInteraction : MonoBehaviour
{
    [Header("Yaz� Ayarlar�")]
    [Tooltip("3D TextMeshPro bile�eni")]
    public TextMeshPro text3D;
    [TextArea]
    [Tooltip("Tabelada g�sterilecek a��klama metni")]
    public string description;

    [Header("Ses Ayarlar�")]
    [Tooltip("�al�nacak ses klibi")]
    public AudioClip voiceClip;
    [Tooltip("Ses �iddetini �arpanla ayarlamak i�in")]
    [Range(0.1f, 3f)]
    public float volumeModifier = 1f;

    AudioSource audioSrc;
    XRSimpleInteractable interactable;

    void Awake()
    {
        // AudioSource referans�n� al, klibi ata
        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = voiceClip;
        audioSrc.playOnAwake = false;
        // spatialBlend / minDistance / maxDistance ayarlar�n� Inspector'dan yapabilirsiniz

        // TextMeshPro i�in description'� y�kle
        if (text3D != null)
            text3D.text = description;

        // XRSimpleInteractable event'lerine abone ol
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExited);
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log($"[Signboard] Playing '{voiceClip?.name}' @ volume x{volumeModifier}");
        audioSrc.PlayOneShot(voiceClip, volumeModifier);
    }

    void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("[Signboard] Stopping audio");
        audioSrc.Stop();
    }

    void LateUpdate()
    {
        // Billboard: +Z ekseni (quad �n y�z�) her zaman kameraya d�ns�n
        var cam = Camera.main;
        if (cam == null) return;

        // Kamera y�n�ne yatay d�zlemde bak�� vekt�r�
        Vector3 d = cam.transform.position - transform.position;
        d.y = 0f;

        // �lk olarak kameraya bakacak rotasyonu al
        Quaternion lookRot = Quaternion.LookRotation(d.normalized, Vector3.up);
        // Sonra 180� Y ekseninde d�nd�r, b�ylece quad'�n +Z y�z� kameraya d�ner
        transform.rotation = lookRot * Quaternion.Euler(0f, 180f, 0f);
    }
}

