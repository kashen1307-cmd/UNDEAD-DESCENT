using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    private string startSceneName = "GameFloor1";

    public void Retry()
    {
        GameManager.instance.ResetRun();

        // keep pistol cause we want need to spawn with da ting
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
        
        if (KeepPlayerAlive.instance != null)
        {
            Destroy(KeepPlayerAlive.instance.gameObject);
            KeepPlayerAlive.instance = null;
        }

        SceneManager.LoadScene("Main Menu");
    }
}
