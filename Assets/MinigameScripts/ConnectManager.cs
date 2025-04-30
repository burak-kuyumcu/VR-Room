using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ConnectManager : MonoBehaviour
{
    [Tooltip("T�m boru segmentlerini buraya at (veya bo� b�rak, otomatik bulunur)")]
    public PipeSegment[] segments;

    [Tooltip("Su efektini tetikleyecek Particle System")]
    public ParticleSystem fountain;

    void Awake()
    {
        // E�er inspector'da elle atamad�ysan, sahnedeki t�m PipeSegment'leri bul
        if (segments == null || segments.Length == 0)
        {
            segments = FindObjectsOfType<PipeSegment>();
            Debug.Log($"ConnectManager: Otomatik olarak {segments.Length} segment bulundu.");
        }

        if (fountain == null)
            Debug.LogWarning("ConnectManager: Fountain ParticleSystem atanmam��!");
    }

    void Update()
    {
        // segments dizisindeki her bir segment'in ger�ek bir obje olup hizal� oldu�unu kontrol et
        bool allAligned = true;
        foreach (var seg in segments)
        {
            if (seg == null || !seg.isAligned)
            {
                allAligned = false;
                break;
            }
        }

        // E�er hepsi hizal�ysa suyu a�, de�ilse kapat
        if (fountain != null)
        {
            if (allAligned)
            {
                if (!fountain.isPlaying)
                    fountain.Play();
            }
            else
            {
                if (fountain.isPlaying)
                    fountain.Stop();
            }
        }
    }
}
