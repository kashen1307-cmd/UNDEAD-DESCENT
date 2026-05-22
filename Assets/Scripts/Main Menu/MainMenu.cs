using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        if (GameManager.instance != null)
            GameManager.instance.ResetRun();
        Debug.Log(
    "Weapon slot 0: " +
    GameManager.instance.savedWeaponPrefabs[0]
);

        if (MusicManager.instance != null)
            MusicManager.instance.RestartMusic();

        // Destroy leftover persistent player
        if (KeepPlayerAlive.instance != null)
        {
            Destroy(KeepPlayerAlive.instance.gameObject);
            KeepPlayerAlive.instance = null;
        }

        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
