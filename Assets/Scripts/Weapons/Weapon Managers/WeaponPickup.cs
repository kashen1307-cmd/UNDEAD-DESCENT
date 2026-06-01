using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    public GameObject handWeaponPrefab; 
    

    public GameObject interactPrompt;    

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
               
                WeaponData data = handWeaponPrefab.GetComponent<WeaponData>();

                if (data != null)
                {
                    
                    playerSwapper.SwapWeapon(handWeaponPrefab, data.floorPickupPrefab);
                    
                    
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
