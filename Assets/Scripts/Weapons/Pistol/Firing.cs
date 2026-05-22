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
    public int maxAmmo = 12;

    public int currentAmmo;

    public float reloadTime = 1.5f;

    private bool isReloading = false;

    [SerializeField]
    private AudioSource gunAudio;

    [SerializeField]
    private AudioClip gunshotClip;

    private PlayerMovement playerStats;


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

        if (reloadText != null)
        {
            reloadText.text = "";
        }
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
            ammoText.text = currentAmmo + " / " + maxAmmo;
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
            reloadText.text = "";
        }

        // Manual reload
        if (Input.GetKeyDown(KeyCode.R)
            && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        // Empty magazine
        if (currentAmmo <= 0)
        {
            if (reloadText != null)
            {
                reloadText.text = "Press R to Reload";
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

    IEnumerator Reload()
    {
        isReloading = true;

        if (reloadText != null)
        {
            reloadText.text = "";
        }

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;

        UpdateAmmoUI();

        if (reloadText != null)
        {
            reloadText.text = ""; ;
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
