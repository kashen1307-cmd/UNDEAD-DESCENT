using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    public GameObject SettingsPanel;
    public ItemInventory inventoryUI;

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

    public void Resume()
    {
        pauseMenuUI.SetActive(false);

        Time.timeScale = 1f;
        AudioListener.pause = false;

        isPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void OpenSettings()
    {
        SettingsPanel.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void CloseSettings()
    {
        SettingsPanel.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;
        AudioListener.pause = true;

        isPaused = true;

        if (inventoryUI != null)
        {
            inventoryUI.RefreshInventoryDisplay();
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");

        Time.timeScale = 1f;
        AudioListener.pause = false;

        isPaused = false;
        pauseMenuUI.SetActive(false);

        SceneManager.LoadScene("Main Menu");
    }
}
