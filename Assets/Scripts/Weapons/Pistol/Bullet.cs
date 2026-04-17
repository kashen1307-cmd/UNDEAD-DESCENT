using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject Zombies;

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
        // Handle damage or effects here
        Debug.Log(hitInfo.name);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Zombies.tag == "EnemyMovement")
        {
            Destroy(Zombies); // Deletes the enemy
            Destroy(gameObject);           // Deletes the bullet
        }
    }

}
