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
        // start intro with no weapons
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

       
        if (floorPrefabs[currentSlot] != null)
        {
            Vector3 dropPosition = transform.position + new Vector3(0.2f, -0.2f, 0f);
            Instantiate(floorPrefabs[currentSlot], dropPosition, Quaternion.identity);
        }

        
        if (equippedGuns[currentSlot] != null)
        {
            Destroy(equippedGuns[currentSlot]);
        }

       
        GameObject newHandWeapon = Instantiate(newWeaponPrefab, weaponSocket.position, weaponSocket.rotation);
        newHandWeapon.transform.SetParent(weaponSocket);

       
        equippedGuns[currentSlot] = newHandWeapon;
        floorPrefabs[currentSlot] = newDropPrefab;

       
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
        
       
            if (WeaponHUD.instance != null)
            {
            WeaponHUD.instance.SwapWeapon(gunStats.uiIcon); 
            }
        }
    }
    
    private void UpdateWeaponVisibility()
    {
        
        if (equippedGuns[0] != null) equippedGuns[0].SetActive(currentSlot == 0);
        if (equippedGuns[1] != null) equippedGuns[1].SetActive(currentSlot == 1);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

   
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (equippedGuns[currentSlot] != null)
        {
            
            WeaponData startingGunStats = equippedGuns[currentSlot].GetComponent<WeaponData>();
            
            if (WeaponHUD.instance != null)
            {
                WeaponHUD.instance.SwapWeapon(startingGunStats.uiIcon);
            }
        }
    }

    public void ApplyReloadBuff(float buffAmount)
    {
        
        currentReloadMultiplier *= buffAmount;
    }

    public void RestoreActiveWeaponAmmo(int amount)
    {
       
        if (equippedGuns[currentSlot] != null)
        {
           
            IWeapon activeGunScript = equippedGuns[currentSlot].GetComponent<IWeapon>();

            if (activeGunScript != null)
            {
                
                activeGunScript.AddAmmo(amount);
            }
        }
    }
}
    