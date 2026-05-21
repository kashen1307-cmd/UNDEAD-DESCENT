using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        if (GameManager.instance != null)
            GameManager.instance.ResetRun();

        if (MusicManager.instance != null)
            MusicManager.instance.RestartMusic();

        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
