using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // "Oyuna Ba�la" butonuna ba�lay�n.
    public void PlayGame()
    {
        // Oyun sahnesinin ad�n� veya indeksini girin
        SceneManager.LoadScene("GameScene");
    }

    // "��k��" butonuna ba�lay�n.
    public void QuitGame()
    {
        Application.Quit();
    }
}
