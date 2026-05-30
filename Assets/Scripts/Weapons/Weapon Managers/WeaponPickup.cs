using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    public GameObject handWeaponPrefab; // The gun that shoots  new Shotgun prefab)
    

    public GameObject interactPrompt; // Optional "Press E to Swap" text    

    private bool isPlayerInRange = false;
    private WeaponSwapper playerSwapper;



    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            IntroTutorialManager intro =
    FindAnyObjectByType<IntroTutorialManager>();

            if (intro != null)
            {
                intro.GunPickedUp();
            }

            if (playerSwapper != null)
            {
                // 1. Look at the safe, untouched shooting gun asset and grab its data
                WeaponData data = handWeaponPrefab.GetComponent<WeaponData>();

                if (data != null)
                {
                    // 2. Hand the safe blueprints to the player!
                    playerSwapper.SwapWeapon(handWeaponPrefab, data.floorPickupPrefab);
                    
                    // 3. Destroy this floor clone (The memory is now safe!)
                    Destroy(gameObject); 
                }
                else
                {
                    Debug.LogError("Hey! " + handWeaponPrefab.name + " is missing the WeaponData script!");
                }
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
