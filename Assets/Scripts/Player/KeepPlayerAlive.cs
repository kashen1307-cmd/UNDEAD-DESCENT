using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepPlayerAlive : MonoBehaviour
{
    public static KeepPlayerAlive instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (scene.name == "Main Menu"
            || scene.name == "DeathScreen"
            || scene.name == "EndOfGame")
        {
            Destroy(gameObject);
        }
    }
}
