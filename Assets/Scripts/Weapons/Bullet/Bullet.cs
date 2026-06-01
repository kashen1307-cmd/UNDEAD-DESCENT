using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public GameObject Enemy;
    public GameObject impactPrefab;

    public int damage = 1;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void Setup(int playerDamage)
    {
        damage = playerDamage;
    }

    void Start()
    {
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, 2f);
    }
    
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage); 
        }

        if (hitInfo.CompareTag("Wall"))
        {
            
            Quaternion bounceRotation = transform.rotation * Quaternion.Euler(0, 0, 0);

            
            Instantiate(impactPrefab, transform.position, bounceRotation);
        }

        Destroy(gameObject);
    }
}



