using UnityEngine;
using UnityEngine.EventSystems;

public class VendingMachine : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject interactPrompt;
    private bool isPlayerInRange = false;

    void Update()
    {
        if (!isPlayerInRange) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!shopUI.activeSelf)
            {
                // OPEN SHOP
                shopUI.SetActive(true);

                Time.timeScale = 0f;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                // CLOSE SHOP
                shopUI.SetActive(false);

                Time.timeScale = 1f;

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactPrompt != null && !shopUI.activeSelf)
            {
                interactPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }
            
            if (shopUI != null)
            {
                shopUI.SetActive(false);
            } 
          
            Time.timeScale = 1f;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;

        }

            
    }
}
