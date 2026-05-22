using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    private string startSceneName = "GameFloor1";

    public void Retry()
    {
        GameManager.instance.ResetRun();

        // Starter pistol on retry
        GameManager.instance.savedWeaponPrefabs[0] =
            GameManager.instance.defaultPistolPrefab;

        if (MusicManager.instance != null)
            MusicManager.instance.RestartMusic();

        if (KeepPlayerAlive.instance != null)
        {
            Destroy(KeepPlayerAlive.instance.gameObject);
            KeepPlayerAlive.instance = null;
        }

        SceneManager.LoadScene("GameFloor1");
    }

    public void ExitToMainMenu()
    {
        // Destroy persistent player
        if (KeepPlayerAlive.instance != null)
        {
            Destroy(KeepPlayerAlive.instance.gameObject);
            KeepPlayerAlive.instance = null;
        }

        SceneManager.LoadScene("Main Menu");
    }
}
