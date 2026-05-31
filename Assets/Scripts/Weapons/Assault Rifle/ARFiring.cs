using UnityEngine;
using TMPro;
using System.Collections;

public class ARFiring : MonoBehaviour, IWeapon
{
    [Header("Weapon")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    public int burstCount = 3;
    public float timeBetweenBurstShots = 0.08f;
    public float fireRate = 0.5f;

    private bool isShooting = false;

    [Header("Ammo")]
    public int magazineSize = 30;
    public int currentAmmo;

    public int reserveAmmo = 90;
    public int maxReserveAmmo = 90;

    public float reloadTime = 1.5f;
    private bool isReloading = false;

    [Header("Audio")]
    [SerializeField]
    private AudioSource gunAudio;

    [SerializeField]
    private AudioClip gunshotClip;

    [SerializeField]
    private AudioClip reloadClip;

    [SerializeField]
    private float reloadSoundDelay = 0.5f;

    [SerializeField]
    private AudioClip emptyClickClip;

    private PlayerMovement playerStats;

    public TMP_Text ammoText;
    public TMP_Text reloadText;

    void Start()
    {
        playerStats = FindAnyObjectByType<PlayerMovement>();

        currentAmmo = magazineSize;
        reserveAmmo = maxReserveAmmo - magazineSize;

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

        isReloading = false;
        isShooting = false;

        UpdateAmmoUI();
        RefreshReloadUI();
    }

    void OnDisable()
    {
        if (reloadText != null)
        {
            reloadText.text = "";
        }

        StopAllCoroutines();

        isReloading = false;
        isShooting = false;

        if (gunAudio != null)
        {
            gunAudio.Stop();
        }
    }

    void Update()
    {
        if (Time.timeScale == 0f)
            return;

        FindAmmoUI();
        FindReloadUI();

        RefreshReloadUI(); // ALWAYS runs

        if (isShooting)
            return;

        // Reload input still allowed
        if (Input.GetKeyDown(KeyCode.R)
            && currentAmmo < magazineSize
            && reserveAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // Empty mag logic
        if (currentAmmo <= 0)
        {
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

        // Fire
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ShootBurst());
        }
    }

    void StopGunSound()
    {
        gunAudio.Stop();
    }

    private IEnumerator ShootBurst()
    {
        isShooting = true;

        // Play once when Fire1 is pressed
        if (gunshotClip != null)
        {
            gunAudio.PlayOneShot(gunshotClip);
            CancelInvoke(nameof(StopGunSound));
            Invoke(nameof(StopGunSound), 0.3f);
        }

        for (int i = 0;
             i < burstCount;
             i++)
        {
            if (currentAmmo <= 0)
                break;

            currentAmmo--;
            UpdateAmmoUI();

            GameObject newBullet =
                Instantiate(
                    bulletPrefab,
                    firePoint.position,
                    firePoint.rotation);

            Bullet bulletScript =
                newBullet.GetComponent<Bullet>();

            if (bulletScript != null
                && playerStats != null)
            {
                bulletScript.Setup(
                    playerStats.currentTotalDamage);
            }

            yield return
                new WaitForSeconds(
                    timeBetweenBurstShots);
        }

        yield return
            new WaitForSeconds(fireRate);

        isShooting = false;
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

        StartCoroutine(PlayReloadSoundDelayed());

        float playerMultiplier = 1f;
        WeaponSwapper playerSwapper = GetComponentInParent<WeaponSwapper>();
        if (playerSwapper != null)
        {
            playerMultiplier = playerSwapper.currentReloadMultiplier;
        }
        
        float actualReloadTime = reloadTime * playerMultiplier;

        // --- REPLACE THE OLD WAIT TIMER WITH THIS ---
        yield return new WaitForSeconds(actualReloadTime);

        int ammoNeeded =magazineSize - currentAmmo;

        int ammoToLoad =Mathf.Min(ammoNeeded,reserveAmmo);

        currentAmmo += ammoToLoad;
        reserveAmmo -= ammoToLoad;

        isReloading = false;

        UpdateAmmoUI();
    }

    void RefreshReloadUI()
    {
        if (reloadText == null)
            return;

        if (isReloading)
        {
            reloadText.text = "Reloading...";
        }
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

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text =
                currentAmmo +
                " / " +
                reserveAmmo;
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
                    ammoUI.GetComponent<TMP_Text>();
            }
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
            }
        }
    }

    public void AddAmmo(int amount)
    {
        reserveAmmo += amount;
        UpdateAmmoUI(); 
    }
    
}