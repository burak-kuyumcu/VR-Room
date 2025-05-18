
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveDisplayManager : MonoBehaviour
{
    public static ObjectiveDisplayManager Instance;

    [Tooltip("Nested prefab'� g�sterir")]
    public GameObject objectivePrefab;
    [Tooltip("World-space UI objelerini toplamak i�in bo� GameObject (opsiyonel)")]
    public Transform worldUIContainer;

    private ObjectiveDisplay currentDisp;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// World-space'te parent's�z instantiate,
    /// nested wrapper'� atla, do�ru pozisyona koy ve billboard + show yap.
    /// </summary>
    public void ShowObjectiveAbove(Transform target, string text, Vector3 offset)
    {
        // �nceki UI varsa temizle
        if (currentDisp != null)
            Destroy(currentDisp.gameObject);

        // 1) Prefab� sahne k�k�nde parent's�z instantiate et
        var root = Instantiate(objectivePrefab);

        // 2) Direkt d�nya-koordinat�na pozisyon ayarla
        root.transform.position = target.position + offset;

        // 3) Ger�ek ObjectiveDisplay bile�enini bul (root veya child)
        var disp = root.GetComponent<ObjectiveDisplay>()
                ?? root.GetComponentInChildren<ObjectiveDisplay>();
        if (disp == null)
        {
            Debug.LogError("ObjectiveDisplay component'i bulunamad�! Prefab'� kontrol et.");
            Destroy(root);
            return;
        }

        // 4) E�er disp, root'un child'� ise wrapper'� sil
        if (disp.transform != root.transform)
        {
            disp.transform.SetParent(null, true); // sahne k�k�ne al
            Destroy(root);                        // wrapper'� kald�r
        }

        // 5) Canvas event camera ayar�
        var canvas = disp.GetComponent<Canvas>();
        if (canvas != null && Camera.main != null)
            canvas.worldCamera = Camera.main;

        // 6) Billboard: UI her zaman kameraya baks�n
        if (Camera.main != null)
            disp.transform.rotation = Quaternion.LookRotation(
                disp.transform.position - Camera.main.transform.position);

        // 7) FollowTarget ve Show text
        disp.followTarget = target;
        disp.localOffset = offset;
        disp.Show(text);

        currentDisp = disp;
    }

    /// <summary>
    /// E�er spawn edilmi� UI varsa status text'i g�nceller.
    /// </summary>
    public void UpdateStatus(int current, int total)
    {
        if (currentDisp != null)
            currentDisp.UpdateStatus(current, total);
    }

    /// <summary>
    /// Mevcut UI'� gizler ve yok eder.
    /// </summary>
    public void HideObjective()
    {
        if (currentDisp != null)
        {
            currentDisp.Hide();
            Destroy(currentDisp.gameObject, 0.2f);
            currentDisp = null;
        }
    }
}

