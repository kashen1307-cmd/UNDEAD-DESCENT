using UnityEngine;

public class ShotgunFiring : MonoBehaviour
{   
    public Transform firePoint;
    public GameObject bulletPrefab;

    public int pelletsPerShot = 5;
    public float spreadAngle = 30f; // Total spread angle in degrees
    public float fireRate = 1f; // Time between shots in seconds

    private float nextFireTime = 0f;
     
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            // Only shoot if the game isn't paused by the vending machine
            if (Time.timeScale > 0)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        float angleStep = spreadAngle / (pelletsPerShot - 1);
        float startingAngle = -spreadAngle / 2;

        for (int i = 0; i < pelletsPerShot; i++)
        {
            float currentAngle = startingAngle + (angleStep * i);
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, currentAngle);
            Instantiate(bulletPrefab, firePoint.position, rotation);
        }
    }
}
