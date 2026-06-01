using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public ItemSO itemSO;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    [SerializeField]
    private AudioSource pickupAudio;

    [SerializeField]
    private AudioClip pickupClip;

    

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
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("The item just touched: " + collision.gameObject.name);
        
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true; 

            ApplyEffect(collision.gameObject);

            GameManager.instance.AddItemToInventory(itemSO);

            if (animator != null)
            {
                animator.Play("PickupAnimation"); 
            }

            if (pickupAudio != null &&
                pickupClip != null)
            {
                pickupAudio.clip = pickupClip;
                pickupAudio.Play();

                CancelInvoke(nameof(StopPickupSound));
                Invoke(nameof(StopPickupSound), 0.5f);
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
            
            PlayerMovement playerStats = playerObject.GetComponent<PlayerMovement>();
            if (playerStats != null)
            {
                playerStats.currentTotalDamage += itemSO.damageBonus;
               
               if (playerStats.currentTotalDamage > 5) 
                {
                    playerStats.currentTotalDamage = 5;
                }

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
            
            wallet.coinMultiplier += itemSO.coinMultiplierBonus; 
            }
            }


        if (itemSO.reloadSpeedMultiplier != 0)
        {
            WeaponSwapper weaponSwapper = playerObject.GetComponent<WeaponSwapper>();
        
            if (weaponSwapper != null)
            {
                weaponSwapper.ApplyReloadBuff(itemSO.reloadSpeedMultiplier);
                 Debug.Log("New reload speed: " + weaponSwapper.currentReloadMultiplier);
            }
            else
            {
                Debug.LogWarning("WeaponSwapper not found on the Player!");
            }
        }

        if (itemSO.ammoRestoreAmount > 0)
        {
            WeaponSwapper weaponSwapper = playerObject.GetComponent<WeaponSwapper>();
        
            if (weaponSwapper != null)
            {
                
                weaponSwapper.RestoreActiveWeaponAmmo(itemSO.ammoRestoreAmount); 
            }
        }  

        if (itemSO.dashCooldownReduction > 0f)
        {
            
            PlayerDash playerDash = playerObject.GetComponent<PlayerDash>();
        
            if (playerDash != null)
            {
                playerDash.ReduceDashCooldown(itemSO.dashCooldownReduction);
            }
        }
     

    }

    void StopPickupSound()
    {
        if (pickupAudio != null)
        {
            pickupAudio.Stop();
        }
    }
}
