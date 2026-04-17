using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public GameObject Enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Move the bullet forward based on its rotation
        rb.linearVelocity = transform.right * speed;
        // Destroy bullet after 2 seconds to clean up the scene
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            Destroy(hitInfo.gameObject); // only enemies
           
        }

        Destroy(gameObject);         // destroy bullet
    }
}


