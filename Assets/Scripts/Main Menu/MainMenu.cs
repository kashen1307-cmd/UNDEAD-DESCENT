using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("GameFloor1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
