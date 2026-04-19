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
    }
}
