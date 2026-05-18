using UnityEngine;
using System.Collections;
using TMPro;

public class Firing : MonoBehaviour
{
    [Header("Ammo UI")]
    public TMP_Text ammoText;

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

        ammoText = GameObject.Find("AmmoText").GetComponent<TMP_Text>();

        UpdateAmmoUI();
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
        if (Time.timeScale == 0f)
            return;

        // Cannot shoot while reloading
        if (isReloading)
            return;

        // Auto reload when empty
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
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
