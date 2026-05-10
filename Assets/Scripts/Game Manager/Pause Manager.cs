using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    
    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    
// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Must be 'public' so our buttons can click it!
    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide the menu
        Time.timeScale = 1f;          // Unfreeze time
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);  // Show the menu
        Time.timeScale = 0f;          // Freeze time perfectly to 0
        isPaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Must be 'public' so our buttons can click it!
    public void QuitGame()
    {
        Debug.Log("Quitting game"); 
        
        Time.timeScale = 1f; 
        isPaused = false;  
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("Main Menu");           // This actually closes the game when you build the .exe!
    
    }
}
