using UnityEngine;

public class Firing : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float fireRate = 5f; // bullets per second
    private float nextFireTime = 0f;

    [SerializeField] 
    private AudioSource gunAudio;

    [SerializeField]
    private AudioClip gunshotClip;


    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
    void StopGunSound()
    {
        gunAudio.Stop();
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        gunAudio.clip = gunshotClip;
        gunAudio.Play();
        Invoke(nameof(StopGunSound), 0.5f);
    }
}
