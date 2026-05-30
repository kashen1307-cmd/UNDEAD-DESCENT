using UnityEngine;
using System.Collections;
using TMPro;

public class Firing : MonoBehaviour
{
    [Header("Ammo UI")]
    public TMP_Text ammoText;

    public TMP_Text reloadText;

    public Transform firePoint;

    public GameObject bulletPrefab;

    public float fireRate = 5f;

    private float nextFireTime = 0f;

    [Header("Ammo")]

    public int magazineSize = 6;
    public int currentAmmo;

    public int maxReserveAmmo = 32;
    public int reserveAmmo;

    public float reloadTime = 1.5f;

    private bool isReloading = false;

    [SerializeField]
    private AudioSource gunAudio;

    [SerializeField]
    private AudioClip gunshotClip;

    [SerializeField]
    private AudioClip emptyClickClip;

    [SerializeField] 
    private AudioClip reloadClip;

    [SerializeField]
    private float reloadSoundDelay = 0.5f;

    private float nextEmptyClickTime = 0f;

    private PlayerMovement playerStats;


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

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text =
            currentAmmo + " / " + reserveAmmo;
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
        && currentAmmo < magazineSize
        && reserveAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // Empty magazine
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
                    reloadText.text = "Out of Ammo!";
                }
            }

            if (Input.GetButtonDown("Fire1") && Time.time > nextEmptyClickTime)
            {
                PlayEmptyClick();
                nextEmptyClickTime = Time.time + 0.3f; // small cooldown
            }

            return;
        }

        if (Input.GetButton("Fire1")
            && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void PlayEmptyClick()
    {
        if (gunAudio != null && emptyClickClip != null)
        {
            gunAudio.pitch = Random.Range(0.95f, 1.1f);
            gunAudio.PlayOneShot(emptyClickClip);
            CancelInvoke(nameof(StopGunSound));
            Invoke(nameof(StopGunSound), 0.5f);
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

        StartCoroutine(
        PlayReloadSoundDelayed());

        yield return new WaitForSeconds(reloadTime);

        int bulletsNeeded =
            magazineSize - currentAmmo;

        int bulletsToLoad =
            Mathf.Min(bulletsNeeded,
                      reserveAmmo);

        currentAmmo += bulletsToLoad;
        reserveAmmo -= bulletsToLoad;

        isReloading = false;

        UpdateAmmoUI();
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

    void StopGunSound()
    {
        gunAudio.Stop();
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoUI();
        Debug.Log("Ammo: " + currentAmmo);

        GameObject newBullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Bullet bulletScript = newBullet.GetComponent<Bullet>();

        if (bulletScript != null && playerStats != null)
        {
            bulletScript.Setup(playerStats.currentTotalDamage);
        }

        gunAudio.clip = gunshotClip;
        gunAudio.Play();
        Invoke(nameof(StopGunSound), 0.5f);
    }

    
}
