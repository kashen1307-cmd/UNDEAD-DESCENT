using UnityEngine;
using TMPro;
using System.Collections;

public class ShotgunFiring : MonoBehaviour, IWeapon
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Shotgun")]
    public int pelletsPerShot = 5;
    public float spreadAngle = 30f;
    public float fireRate = 1f;

    [Header("Ammo")]
    public int maxAmmo = 2;  
    public int currentAmmo;

    public int reserveAmmo = 12; 
    public int maxReserveAmmo = 12;

    public float reloadTime = 2f;
    private bool isReloading = false;

    [SerializeField]
    private AudioSource gunAudio;

    [SerializeField]
    private AudioClip gunshotClip;

    [SerializeField] 
    private AudioClip reloadClip;

    [SerializeField]
    private float reloadSoundDelay = 1f;

    [SerializeField]
    private AudioClip emptyClickClip;

    private float nextFireTime = 0f;
    private PlayerMovement playerStats;

    public TMP_Text ammoText;

    public TMP_Text reloadText;

    void Start()
    {
        playerStats = FindAnyObjectByType<PlayerMovement>();

        currentAmmo = maxAmmo;

        GameObject ammoUI =
            GameObject.Find("AmmoText");

        if (ammoUI != null)
        {
            ammoText =
                ammoUI.GetComponent<TMPro.TMP_Text>();

            UpdateAmmoUI();

            
        }

        GameObject reloadUI =
         GameObject.Find("ReloadText");

        if (reloadUI != null)
        {
            reloadText =
                reloadUI.GetComponent<TMP_Text>();

            reloadText.text = "";
        }
    }

    void OnEnable()
    {
        FindAmmoUI();
        FindReloadUI();

        UpdateAmmoUI();
        RefreshReloadUI();
    }

    void OnDisable()
    {
        if (reloadText != null)
        {
            reloadText.text = "";
        }

        CancelInvoke();

        if (gunAudio != null)
        {
            gunAudio.Stop();
        }
    }

    void Update()
    {
        FindAmmoUI();
        FindReloadUI();
        if (Time.timeScale == 0f)
            return;

        if (isReloading)
            return;

        if (reloadText != null && currentAmmo > 0)
        {
            RefreshReloadUI();
        }

        // Manual reload
        if (Input.GetKeyDown(KeyCode.R)
        && currentAmmo < maxAmmo
        && reserveAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }


        // Empty mag
        if (currentAmmo <= 0)
        {
            if (reloadText != null)
            {
                if (reserveAmmo > 0)
                {
                    reloadText.text = "Press R to Reload";
                }
                else
                {
                    reloadText.text = "Out of Ammo";
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if (emptyClickClip != null)
                {
                    gunAudio.PlayOneShot(emptyClickClip);

                    CancelInvoke(nameof(StopGunSound));
                    Invoke(nameof(StopGunSound), 0.5f);
                }
            }

            return;
        }


        if (Input.GetButtonDown("Fire1")
            && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    IEnumerator PlayReloadSoundDelayed()
    {
        yield return new WaitForSeconds(
            reloadSoundDelay);

        if (gunAudio != null &&
            reloadClip != null)
        {
            gunAudio.PlayOneShot(
                reloadClip);
            CancelInvoke(nameof(StopGunSound));
            Invoke(nameof(StopGunSound), 0.5f);
        }
    }



    IEnumerator Reload()
    {
        isReloading = true;

        if (reloadText != null)
        {
            RefreshReloadUI();
        }

        StartCoroutine(PlayReloadSoundDelayed());

        float playerMultiplier = 1f;
        WeaponSwapper playerSwapper = GetComponentInParent<WeaponSwapper>();
        if (playerSwapper != null)
        {
            playerMultiplier = playerSwapper.currentReloadMultiplier;
        }
        
        float actualReloadTime = reloadTime * playerMultiplier;

        if (playerSwapper != null)
        {
            
            ReloadUI playerReloadUI = playerSwapper.GetComponentInChildren<ReloadUI>();
            
            if (playerReloadUI != null)
            {
                playerReloadUI.StartReloadBar(actualReloadTime);
            }
            else
            {
                Debug.LogWarning("The Player couldn't find the ReloadUI script!");
            }
        }

       
        yield return new WaitForSeconds(actualReloadTime);

        int ammoNeeded = maxAmmo - currentAmmo;

        int ammoToLoad = Mathf.Min(ammoNeeded, reserveAmmo);

        currentAmmo += ammoToLoad;
        reserveAmmo -= ammoToLoad;

        isReloading = false;

        UpdateAmmoUI();

        if (reloadText != null)
        {
            reloadText.text = "";
        }
    }

    void FindReloadUI()
    {
        if (reloadText == null)
        {
            GameObject reloadUI =
                GameObject.Find("ReloadText");

            if (reloadUI != null)
            {
                reloadText =
                    reloadUI.GetComponent<TMP_Text>();

                reloadText.text = "";

                
            }
        }
    }

    void RefreshReloadUI()
    {
        if (reloadText == null)
            return;

       
        else if (currentAmmo <= 0)
        {
            if (reserveAmmo > 0)
            {
                reloadText.text = "Press R to Reload";
            }
            else
            {
                reloadText.text = "Out of Ammo";
            }
        }
        else
        {
            reloadText.text = "";
        }
    }

    void FindAmmoUI()
    {
        if (ammoText == null)
        {
            GameObject ammoUI =
                GameObject.Find("AmmoText");

            if (ammoUI != null)
            {
                ammoText =
                    ammoUI.GetComponent<TMPro.TMP_Text>();

                UpdateAmmoUI();

                
            }
        }
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text =
                currentAmmo + " / " + reserveAmmo;
        }
    }

    void StopGunSound()
    {
        gunAudio.Stop();
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoUI();

        float angleStep = spreadAngle / (pelletsPerShot - 1);
        float startingAngle = -spreadAngle / 2;

        for (int i = 0; i < pelletsPerShot; i++)
        {
            float currentAngle = startingAngle + (angleStep * i);

            Quaternion rotation =
                firePoint.rotation *
                Quaternion.Euler(0, 0, currentAngle);

            GameObject newPellet =
                Instantiate(
                    bulletPrefab,
                    firePoint.position,
                    rotation
                );

            Bullet bulletScript =
                newPellet.GetComponent<Bullet>();

            if (bulletScript != null && playerStats != null)
            {
                bulletScript.Setup(playerStats.currentTotalDamage);
            }
        }

        gunAudio.clip = gunshotClip;
        gunAudio.Play();

        Invoke(nameof(StopGunSound), 0.5f);
    }

    public void AddAmmo(int amount)
    {
        reserveAmmo += amount;
        UpdateAmmoUI(); 
    }

    

}
