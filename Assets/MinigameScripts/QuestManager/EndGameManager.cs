using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    [Header("Fade Settings")]
    [Tooltip("FadeScreen prefab�� (Quad + FadeScreen script)")]
    public FadeScreen fadeScreenPrefab;

    [Header("Main Menu")]
    [Tooltip("Build Settings�e eklenmi� Ana Men� sahnesinin ad�")]
    public string mainMenuSceneName = "1 Start Scene";

    private FadeScreen _fadeScreenInstance;

    void Awake()
    {
        // 1) Scene�de do�rudan sahnede de�il, prefab�dan klonla
        var go = Instantiate(fadeScreenPrefab.gameObject);
        _fadeScreenInstance = go.GetComponent<FadeScreen>();

        // 2) Klon mutlaka aktif olsun
        go.SetActive(true);
    }

    public void OnGameCompleted()
    {
        StartCoroutine(EndSequence());
    }

    private IEnumerator EndSequence()
    {
        // 1) Fade-out
        _fadeScreenInstance.FadeOut();
        yield return new WaitForSeconds(_fadeScreenInstance.fadeDuration);

        // 2) Ana Men� sahnesini y�kle
        SceneManager.LoadScene(mainMenuSceneName);
    }
}

