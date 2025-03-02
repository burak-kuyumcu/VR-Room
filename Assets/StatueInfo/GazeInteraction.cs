using UnityEngine;

public class GazeInteraction : MonoBehaviour
{
    public float gazeDuration = 1f;  // Panelin a��lmas� i�in gereken s�re
    private float gazeTimer = 0f;

    // Heykel bilgi panelleri
    public GameObject StatueInfo1;
    public GameObject StatueInfo2;
    public GameObject StatueInfo3;
    public GameObject StatueInfo4;

    private Transform vrCamera;   // VR kameras�
    private bool isGazing = false;
    private GameObject currentPanel = null; // Aktif paneli takip etmek i�in

    void Start()
    {
        vrCamera = Camera.main.transform;

        // Ba�lang��ta t�m panelleri kapat
        HideAllPanels();
    }

    void Update()
    {
        Ray ray = new Ray(vrCamera.position, vrCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10f)) // 10 metreye kadar tarama yap
        {
            switch (hit.collider.tag)
            {
                case "Sophia":
                    HandleGaze(StatueInfo1);
                    break;
                case "Apeth":
                    HandleGaze(StatueInfo2);
                    break;
                case "Ennoia":
                    HandleGaze(StatueInfo3);
                    break;
                case "Episteme":
                    HandleGaze(StatueInfo4);
                    break;
                default:
                    ResetGaze();
                    break;
            }
        }
        else
        {
            ResetGaze();
        }

        // E�er bir panel a��ksa, s�rekli kameraya d�n�k olmas�n� sa�la
        if (currentPanel != null)
        {
            currentPanel.transform.LookAt(vrCamera);
            //currentPanel.transform.rotation = Quaternion.Euler(0, currentPanel.transform.rotation.eulerAngles.y, 0); // Sadece yatay eksende d�nd�r

            // 180 derece d�nd�rerek yaz�n�n ters g�r�nmesini engelle
            currentPanel.transform.Rotate(0, 180, 0);
        }
    }

    void HandleGaze(GameObject targetPanel)
    {
        if (currentPanel != targetPanel)
        {
            ResetGaze(); // �nceki a��k paneli kapat
            currentPanel = targetPanel;
            gazeTimer = 0f; // S�reyi s�f�rla
        }

        gazeTimer += Time.deltaTime;
        if (gazeTimer >= gazeDuration && !isGazing)
        {
            ShowPanel(targetPanel);
            isGazing = true;
        }
    }

    void ShowPanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    void ResetGaze()
    {
        gazeTimer = 0f;
        HideAllPanels();
        isGazing = false;
        currentPanel = null;
    }

    void HideAllPanels()
    {
        if (StatueInfo1) StatueInfo1.SetActive(false);
        if (StatueInfo2) StatueInfo2.SetActive(false);
        if (StatueInfo3) StatueInfo3.SetActive(false);
        if (StatueInfo4) StatueInfo4.SetActive(false);
    }
}
