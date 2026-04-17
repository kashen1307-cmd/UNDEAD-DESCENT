

using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("GameFloor1");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
