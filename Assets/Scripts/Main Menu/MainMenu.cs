using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetRun();

            if (GameManager.instance.savedWeaponPrefabs != null &&
                GameManager.instance.savedWeaponPrefabs.Length > 0)
            {
                Debug.Log("Weapon slot 0: " +
                    GameManager.instance.savedWeaponPrefabs[0]);
            }
            else
            {
                Debug.Log("No weapons saved");
            }
        }
        else
        {
            Debug.LogWarning("GameManager instance is NULL");
        }

        if (MusicManager.instance != null)
            MusicManager.instance.RestartMusic();

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
