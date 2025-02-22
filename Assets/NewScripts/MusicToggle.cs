using UnityEngine;

public class MusicToggle : MonoBehaviour
{
    // Arka plan m�zi�ini i�eren AudioSource bile�eni referans�
    public AudioSource musicSource;

    void Update()
    {
        // "M" tu�una bas�ld���nda tetiklenir
        if (Input.GetKeyDown(KeyCode.M))
        {
            // E�er m�zik �al�yorsa duraklat, de�ilse ba�lat
            if (musicSource.isPlaying)
            {
                musicSource.Pause(); // Duraklat�r (son konumundan devam edebilir)
            }
            else
            {
                musicSource.Play(); // M�zik �almaya ba�lar
            }
        }
    }
}
