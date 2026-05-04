

using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{

    [SerializeField] 
    private string startSceneName = "GameFloor1";



    public void Retry()
    {

        GameManager.instance.ResetRun();
        SceneManager.LoadScene("GameFloor1");
    }


    public void ExitToMainMenu()
    {
        
        SceneManager.LoadScene("Main Menu");
    }
}
