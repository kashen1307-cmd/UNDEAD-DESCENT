using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public ItemSO itemSO;
    public SpriteRenderer spriteRenderer;
    public Animator animator; 

    //public int quantity;

    private bool isCollected = false;

    private void OnValidate()
    {
        if (itemSO == null)
            return;

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = itemSO.icon;
        }
        //this.name = itemSO.itemName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true; 

            // 1. Inject the stats into the player
            ApplyEffect(collision.gameObject);

            // 2. Play the visual animation
            if (animator != null)
            {
                animator.Play("PickupAnimation"); 
            }
            
            // 3. Destroy the item after 0.5 seconds
            Destroy(gameObject, 0.5f);
        }
    }

    private void ApplyEffect(GameObject playerObject)
    {
        // --- HEALTH CHECK ---
        if (itemSO.currentHealth > 0)
        {
            // We search for the exact script name your teammate used
            PlayerHealthController playerHealth = playerObject.GetComponent<PlayerHealthController>();
            
            if (playerHealth != null)
            {
                // We call the custom Heal method we added to their script
                playerHealth.SmallItemHeal(itemSO.currentHealth);
            }
            else
            {
                Debug.LogWarning("PlayerHealthController not found on the Player!");
            }
        }
        
        // --- SPEED CHECK ---
        if (itemSO.movementSpeed != 0) 
        {
            // We search for the exact movement script name
            PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
            
            if (playerMovement != null)
            {
                // We call the custom speed method
                playerMovement.IncreaseSpeed(itemSO.movementSpeed);
            }
            else
            {
                Debug.LogWarning("PlayerMovement not found on the Player!");
            }
        }
    }
}
