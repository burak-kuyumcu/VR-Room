using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class StatueInfo : MonoBehaviour // Class public olmal�
{
    public GameObject infoPanel;

    private void Start()
    {
        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    public void ShowInfo() // Fonksiyon public olmal�
    {
        if (infoPanel != null)
            infoPanel.SetActive(true);
    }

    public void HideInfo() // Fonksiyon public olmal�
    {
        if (infoPanel != null)
            infoPanel.SetActive(false);
    }
}
