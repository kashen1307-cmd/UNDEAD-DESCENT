using UnityEngine;

public class WeaponSwapper : MonoBehaviour
{
    
    public Transform weaponSocket;          
    public GameObject currentEquippedWeapon; 
    public GameObject currentDropPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

        if (GameManager.instance.savedWeaponPrefab != null)
        {
            // Remove whatever weapon spawned with player
            if (currentEquippedWeapon != null)
            {
                Destroy(currentEquippedWeapon);
            }

            // Spawn saved weapon
            currentEquippedWeapon = Instantiate(
                GameManager.instance.savedWeaponPrefab,
                weaponSocket.position,
                weaponSocket.rotation
            );

            currentEquippedWeapon.transform.SetParent(weaponSocket);
            currentDropPrefab = GameManager.instance.savedDropPrefab;

            Debug.Log("Equipped = " + currentEquippedWeapon.name);
        }
    }
    public void SwapWeapon(GameObject newWeaponPrefab, GameObject newDropPrefab)
    {
        // 1. Drop the old weapon on the floor
        if (currentDropPrefab != null)
        {
            // Drop it slightly to the side so you don't instantly pick it up again
            Vector3 dropPosition = transform.position + new Vector3(0.2f, -0.2f, 0f);
            Instantiate(currentDropPrefab, dropPosition, Quaternion.identity);
        }

        // 2. Destroy the old weapon from your hands
        if (currentEquippedWeapon != null)
        {
            Destroy(currentEquippedWeapon);
        }

        // 3. Spawn the new weapon and attach it to the player
        currentEquippedWeapon = Instantiate(newWeaponPrefab, weaponSocket.position, weaponSocket.rotation);
        currentEquippedWeapon.transform.SetParent(weaponSocket);

        // 4. Update memory so the game knows what to drop next time
        currentDropPrefab = newDropPrefab;

        GameManager.instance.savedWeaponPrefab = newWeaponPrefab;
        GameManager.instance.savedDropPrefab = newDropPrefab;


        
    }
}
