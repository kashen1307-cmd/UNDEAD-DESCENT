using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public GameObject Enemy;

    public int damage = 1;

    private bool isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rb.linearVelocity = transform.right * speed;
        
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    

 

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage); // 👈 use variable instead of hardcoded value
        }

        Destroy(gameObject);
    }
}



