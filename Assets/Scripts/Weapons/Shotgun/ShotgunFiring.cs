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
    public int maxAmmo = 4;
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

    void Start()
    {
        playerStats = FindAnyObjectByType<PlayerMovement>();

        currentAmmo = maxAmmo;

        ammoText = GameObject.Find("AmmoTextShotgun").GetComponent<TMP_Text>();

        UpdateAmmoUI();
    }

    void Update()
    {
        if (Time.timeScale == 0f)
            return;

        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        if (ammoText != null)
            ammoText.text = "Reloading...";

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;

        UpdateAmmoUI();
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
