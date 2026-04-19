using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public GameObject shopUI;

    public GameObject interactPrompt;

    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            bool isShopOpen = shopUI.activeSelf;
            shopUI.SetActive(!isShopOpen);
            
            // NEW: Hide the prompt if the shop opens. Show it if they close the shop with E.
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(isShopOpen); 
            }
            
            // Pause/Unpause
            Time.timeScale = isShopOpen ? 1f : 0f;
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
        }

        shopUI.SetActive(false);
        Time.timeScale = 1f;    
    }
}
