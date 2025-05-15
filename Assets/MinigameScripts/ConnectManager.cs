using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConnectManager : MonoBehaviour
{
    public PipeSegment[] segments;      // Sahnedeki PipeSegment bile�enleri
    public ParticleSystem fountain;     // Su efektini oynatacak sistem

    void Update()
    {
        // Her par�a tam 90� katlar�na d�n�k m� diye kontrol et
        bool allAligned = segments.All(s =>
            Mathf.Approximately(
                Mathf.Round(s.transform.eulerAngles.y / 90) * 90,
                s.transform.eulerAngles.y));

        if (allAligned && !fountain.isPlaying)
            fountain.Play();
        else if (!allAligned && fountain.isPlaying)
            fountain.Stop();
    }
}
