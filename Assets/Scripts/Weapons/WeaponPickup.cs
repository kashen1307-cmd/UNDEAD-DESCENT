using UnityEngine;




public class WeaponPickup : MonoBehaviour
{

    public GameObject handWeaponPrefab; // The gun that shoots  new Shotgun prefab)
    public GameObject myPickupPrefab;   // This exact floor prefab itself

    public GameObject interactPrompt; // Optional "Press E to Swap" text    

    private bool isPlayerInRange = false;
    private WeaponSwapper playerSwapper;



    void Update()
    {
        // If player is touching it and hits E
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {   
            if (playerSwapper != null)
            {
                // Tell the player to swap, passing in the hand gun and the floor gun
                playerSwapper.SwapWeapon(handWeaponPrefab, myPickupPrefab);
                
                // Destroy this pickup from the floor
                Destroy(gameObject); 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerSwapper = collision.GetComponent<WeaponSwapper>();
            
            if (interactPrompt != null) interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            playerSwapper = null;
            
            if (interactPrompt != null) interactPrompt.SetActive(false);
        }
    }
}
