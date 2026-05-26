using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public ItemSO itemSO;
    public SpriteRenderer spriteRenderer;
    public Animator animator; 

    //public int quantity;

    private PlayerMovement playerStats;

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
        Debug.Log("The item just touched: " + collision.gameObject.name);
        
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true; 

            ApplyEffect(collision.gameObject);


            if (animator != null)
            {
                animator.Play("PickupAnimation"); 
            }

            Destroy(gameObject, 0.5f);
        }
    }

    private void ApplyEffect(GameObject playerObject)
    {
       
        if (itemSO.currentHealth > 0)
        {
       
            PlayerHealthController playerHealth = playerObject.GetComponent<PlayerHealthController>();
            
            if (playerHealth != null)
            {
                playerHealth.SmallItemHeal(itemSO.currentHealth);
            }
            else
            {
                Debug.LogWarning("PlayerHealthController not found on the Player!");
            }
        }
        
        
        if (itemSO.movementSpeed != 0) 
        {
            
            PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
            
            if (playerMovement != null)
            {
               
                playerMovement.IncreaseSpeed(itemSO.movementSpeed);
            }
            else
            {
                Debug.LogWarning("PlayerMovement not found on the Player!");
            }
        }

        if (itemSO.damageBonus != 0)
        {
            // We grab the PlayerMovement script because that is where our currentTotalDamage variable lives right now!
            PlayerMovement playerStats = playerObject.GetComponent<PlayerMovement>();
            if (playerStats != null)
            {
                playerStats.currentTotalDamage += itemSO.damageBonus;
                Debug.Log("Damage Upgraded! New Damage: " + playerStats.currentTotalDamage);
            }
            else
            {
                Debug.LogWarning("PlayerMovement not found for Damage Buff!");
            }
        }

        PlayerInventory inventory = playerObject.GetComponent<PlayerInventory>();
        
        if (inventory != null)
        {
            // Hand the blueprint (ItemSO) over to the backpack to remember it
            inventory.AddItemToInventory(itemSO); 
        }
        else
        {
            Debug.LogWarning("PlayerInventory script not found on the Player!");
        }

        if (itemSO.coinMultiplierBonus > 0f)
        {
            PlayerWallet wallet = playerObject.GetComponent<PlayerWallet>();
            if (wallet != null)
            {
            // If the item gives 0.5f, the player now gets 1.5x coins!
            wallet.coinMultiplier += itemSO.coinMultiplierBonus; 
            }
        }
        
    }
}
