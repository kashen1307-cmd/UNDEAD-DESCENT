using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSwapper : MonoBehaviour
{
    
    public Transform weaponSocket;          
    public GameObject[] equippedGuns = new GameObject[2]; 
    public GameObject[] floorPrefabs = new GameObject[2];

    private int currentSlot = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Intro scene starts unarmed
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            if (equippedGuns[0] != null)
            {
                Destroy(equippedGuns[0]);
                equippedGuns[0] = null;
            }

            if (equippedGuns[1] != null)
            {
                Destroy(equippedGuns[1]);
                equippedGuns[1] = null;
            }

            return;
        }

        // Normal weapon loading
        if (GameManager.instance != null)
        {
            for (int i = 0; i < 2; i++)
            {
                if (GameManager.instance.savedWeaponPrefabs[i] != null)
                {
                    if (i == 0 && equippedGuns[0] != null)
                    {
                        Destroy(equippedGuns[0]);
                    }

                    GameObject spawnedGun = Instantiate(
                        GameManager.instance.savedWeaponPrefabs[i],
                        weaponSocket.position,
                        weaponSocket.rotation
                    );

                    spawnedGun.transform.SetParent(weaponSocket);

                    equippedGuns[i] = spawnedGun;
                    floorPrefabs[i] =
                        GameManager.instance.savedDropPrefabs[i];
                }
            }
        }

        UpdateWeaponVisibility();
    }


    void Update()
    {
        // Press Q to switch between primary and secondary
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleWeaponSlot();
        }
    }
    
    public void SwapWeapon(GameObject newWeaponPrefab, GameObject newDropPrefab)
    {
        if (equippedGuns[0] != null && equippedGuns[1] == null && currentSlot == 0)
        {
            currentSlot = 1;
        }
        else if (equippedGuns[1] != null && equippedGuns[0] == null && currentSlot == 1)
        {
            currentSlot = 0;
        }

        // 1. Drop the active weapon onto the floor
        if (floorPrefabs[currentSlot] != null)
        {
            Vector3 dropPosition = transform.position + new Vector3(0.2f, -0.2f, 0f);
            Instantiate(floorPrefabs[currentSlot], dropPosition, Quaternion.identity);
        }

        // 2. Delete the old gun from your hands
        if (equippedGuns[currentSlot] != null)
        {
            Destroy(equippedGuns[currentSlot]);
        }

        // 3. Spawn the new gun in your hands
        GameObject newHandWeapon = Instantiate(newWeaponPrefab, weaponSocket.position, weaponSocket.rotation);
        newHandWeapon.transform.SetParent(weaponSocket);

        // 4. Update memory slots
        equippedGuns[currentSlot] = newHandWeapon;
        floorPrefabs[currentSlot] = newDropPrefab;

        // 5. Save to GameManager so it survives the next floor transition!
        if (GameManager.instance != null)
        {
            GameManager.instance.savedWeaponPrefabs[currentSlot] = newWeaponPrefab;
            GameManager.instance.savedDropPrefabs[currentSlot] = newDropPrefab;
        }

        UpdateWeaponVisibility();
    }

    private void ToggleWeaponSlot()
    {
        // Only swap if the other slot actually has a gun in it
        if (currentSlot == 0 && equippedGuns[1] != null)
        {
            currentSlot = 1;
        }
        else if (currentSlot == 1 && equippedGuns[0] != null)
        {
            currentSlot = 0;
        }

        UpdateWeaponVisibility();
    }
    
    private void UpdateWeaponVisibility()
    {
        // Turn on the gun in the current slot, turn off the other one
        if (equippedGuns[0] != null) equippedGuns[0].SetActive(currentSlot == 0);
        if (equippedGuns[1] != null) equippedGuns[1].SetActive(currentSlot == 1);
    }
}
    