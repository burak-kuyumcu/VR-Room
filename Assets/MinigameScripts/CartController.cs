using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // E�er saya� g�stereceksen

public class CartController : MonoBehaviour
{
    public int cargoCount = 0;
    public TMP_Text statusText;  // �stersen UI Text referans� ekle

    public void AddCargo()
    {
        cargoCount++;
        if (statusText != null)
            statusText.text = $"{cargoCount}/5 paket y�klendi";
        // 5�e ula��ld���nda ekstra efekt ekleyebilirsin
    }
}

