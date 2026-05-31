using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSwapper : MonoBehaviour
{
    
    public Transform weaponSocket;          
    public GameObject[] equippedGuns = new GameObject[2]; 
    public GameObject[] floorPrefabs = new GameObject[2];


    public float currentReloadMultiplier = 1.0f;

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
        
        WeaponData newGunStats = newWeaponPrefab.GetComponent<WeaponData>();
        if (WeaponHUD.instance != null) 
        {
            WeaponHUD.instance.SwapWeapon(newGunStats.uiIcon);
        }

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

        if (equippedGuns[currentSlot] != null)
        {
            WeaponData gunStats = equippedGuns[currentSlot].GetComponent<WeaponData>(); 
        
        // SAFETY CHECK: Does the UI actually exist in this scene?
            if (WeaponHUD.instance != null)
            {
            WeaponHUD.instance.SwapWeapon(gunStats.uiIcon); 
            }
        }
    }
    
    private void UpdateWeaponVisibility()
    {
        // Turn on the gun in the current slot, turn off the other one
        if (equippedGuns[0] != null) equippedGuns[0].SetActive(currentSlot == 0);
        if (equippedGuns[1] != null) equippedGuns[1].SetActive(currentSlot == 1);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 2. Stop listening if the player dies completely
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 3. This fires automatically the exact millisecond you enter a new apartment floor!
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if we actually have a gun in our hands
        if (equippedGuns[currentSlot] != null)
        {
            // Grab the clean icon and force the brand new UI to show it!
            WeaponData startingGunStats = equippedGuns[currentSlot].GetComponent<WeaponData>();
            
            if (WeaponHUD.instance != null)
            {
                WeaponHUD.instance.SwapWeapon(startingGunStats.uiIcon);
            }
        }
    }

    public void ApplyReloadBuff(float buffAmount)
    {
        // Multiply the current stat by the item's buff. 
        // If they pick up two 0.85 buffs, they compound perfectly!
        currentReloadMultiplier *= buffAmount;
    }

    public void RestoreActiveWeaponAmmo(int amount)
    {
        // 1. Make sure we are actually holding a gun!
        if (equippedGuns[currentSlot] != null)
        {
            // 2. Grab your teammate's firing script off the active gun
            // IMPORTANT: Change 'WeaponFireScript' to whatever your teammate actually named their script!
            IWeapon activeGunScript = equippedGuns[currentSlot].GetComponent<IWeapon>();

            if (activeGunScript != null)
            {
                // 3. Shove the ammo into the gun!
                activeGunScript.AddAmmo(amount);
            }
        }
    }
}
    