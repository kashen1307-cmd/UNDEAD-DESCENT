using UnityEngine;

public class Firing : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        { // Default is Left Mouse Button
            Shoot();
        }

        void Shoot()
        {
            // Instantiate the bullet at the FirePoint's position and rotation
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}

