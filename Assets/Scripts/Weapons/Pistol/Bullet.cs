using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public GameObject Enemy;

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
        if (hitInfo.CompareTag("AreaReveal") || hitInfo.CompareTag("Player") || hitInfo.CompareTag("Bullet"))
    {
        return; 
    }


        if (hitInfo.CompareTag("Enemy"))
        {
            Destroy(hitInfo.gameObject);
            EnemyCounterUI.enemiesAlive--;

            if (EnemyCounterUI.enemiesAlive < 0)
            {
                EnemyCounterUI.enemiesAlive = 0;
            }
        }

    
            Destroy(gameObject);         
       
    }
}


