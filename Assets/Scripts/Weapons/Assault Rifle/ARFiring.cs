using UnityEngine;
using System.Collections;

public class ARFiring : MonoBehaviour
{
    
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int burstCount = 3; 
    public float timeBetweenBurstShots = 0.08f; 
    public float fireRate = 0.5f;

    private bool isShooting = false; 
    private PlayerMovement playerStats;
    
    void Start()
    {
        playerStats = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isShooting)
        {
            StartCoroutine(ShootBurst());
        }   
    }

    private IEnumerator ShootBurst()
    {
        isShooting = true;

        for (int i = 0; i < burstCount; i++)
        {
            // 1. Spawn the Bullet
            GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = newBullet.GetComponent<Bullet>();

            // 2. Inject the Damage Buff!
            if (bulletScript != null && playerStats != null)
            {
                bulletScript.Setup(playerStats.currentTotalDamage);
     
           }

            yield return new WaitForSeconds(timeBetweenBurstShots);
        }

        yield return new WaitForSeconds(fireRate);
        isShooting = false;
    }    


        
        
        
}
