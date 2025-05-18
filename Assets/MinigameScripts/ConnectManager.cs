using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectManager : MonoBehaviour
{
    [Tooltip("S�ras�yla t�m PipeSegment EndPoint'leri")]
    public Transform[] endPoints;
    [Tooltip("Tamamlan�nca patlayacak particle")]
    public ParticleSystem fountain;
    [Tooltip("Quest tetikleyici")]
    public QuestManager questManager;
    [Tooltip("ScriptableObject�teki G�rev Ba�l���")]
    public string questTitle = "Borular� Ba�la";

    [Tooltip("Birbirine d�n�kl�k tolerans� (derece)")]
    public float angleTolerance = 15f;
    [Tooltip("U�lar aras� maksimum mesafe (metre)")]
    public float distanceTolerance = 0.15f;

    private bool started = false;

    void Update()
    {
        bool allConnected = true;

        for (int i = 0; i < endPoints.Length - 1; i++)
        {
            Transform a = endPoints[i];
            Transform b = endPoints[i + 1];

            // 1) U�lar aras� mesafe kontrol�
            float dist = Vector3.Distance(a.position, b.position);
            if (dist > distanceTolerance)
            {
                allConnected = false;
                break;
            }

            // 2) Y�n kontrol�: a'n�n forward'u b'ye bak�yor mu?
            Vector3 toNext = (b.position - a.position).normalized;
            float angleA = Vector3.Angle(a.forward, toNext);
            if (angleA > angleTolerance)
            {
                allConnected = false;
                break;
            }

            // 3) Ayr�ca b'nin forward'u da a'ya bakmal�
            float angleB = Vector3.Angle(b.forward, -toNext);
            if (angleB > angleTolerance)
            {
                allConnected = false;
                break;
            }
        }

        if (allConnected && !started)
        {
            started = true;
            fountain.Play();
            questManager.Trigger(questTitle);
        }
        else if (!allConnected && fountain.isPlaying)
        {
            fountain.Stop();
        }
    }
}



