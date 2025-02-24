using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeInteraction : MonoBehaviour
{
    public float gazeDuration = 2f;  // Panelin a��lmas� i�in gereken s�re
    private float gazeTimer = 0f;

    public GameObject infoPanel;     // Heykelin bilgisini g�sterecek panel
    private Transform vrCamera;      // VR kameras�
    private bool isGazing = false;

    void Start()
    {
        vrCamera = Camera.main.transform;  // VR kameras�n� al
        if (infoPanel != null)
            infoPanel.SetActive(false);    // Ba�lang��ta panel kapal� olsun
    }

    void Update()
    {
        Ray ray = new Ray(vrCamera.position, vrCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10f)) // 10 metreye kadar tarama yap
        {
            if (hit.collider.CompareTag("Statue")) // E�er obje "Statue" etiketi ta��yorsa
            {
                gazeTimer += Time.deltaTime;
                if (gazeTimer >= gazeDuration)
                {
                    ShowInfo();
                    isGazing = true;
                }
            }
            else
            {
                ResetGaze();
            }
        }
        else
        {
            ResetGaze();
        }
    }

    void ShowInfo()
    {
        if (infoPanel != null && !isGazing)
        {
            infoPanel.SetActive(true);
        }
    }

    void ResetGaze()
    {
        gazeTimer = 0f;
        if (infoPanel != null)
            infoPanel.SetActive(false);
        isGazing = false;
    }
}
