using UnityEngine;
using TMPro;
using System.Collections;

public class ShotgunFiring : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Shotgun")]
    public int pelletsPerShot = 5;
    public float spreadAngle = 30f;
    public float fireRate = 1f;

    [Header("Ammo")]
    public int maxAmmo = 1;
    public int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;

    [SerializeField]
    private AudioSource gunAudio;

    [SerializeField]
    private AudioClip gunshotClip;

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

            Debug.Log("Shotgun UI Connected");
        }

        GameObject reloadUI =
         GameObject.Find("ReloadText");

        if (reloadUI != null)
        {
            reloadText =
                reloadUI.GetComponent<TMP_Text>();

            reloadText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        FindAmmoUI();
        if (Time.timeScale == 0f)
            return;

        if (isReloading)
            return;

        // Manual reload
        if (Input.GetKeyDown(KeyCode.R)
            && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        // Empty
        if (currentAmmo <= 0)
        {
            if (ammoText != null)
                if (reloadText != null)
                {
                    reloadText.gameObject.SetActive(true);
                    reloadText.text = "Press R to Reload";
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

    IEnumerator Reload()
    {
        isReloading = true;

        if (reloadText != null)
        {
            reloadText.gameObject.SetActive(true);
            reloadText.text = "Reloading...";
        }

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;

        UpdateAmmoUI();
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

                Debug.Log("Shotgun UI Reconnected");
            }
        }
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo + " / " + maxAmmo;
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

   


}
