using UnityEngine;

public class SpitProjectile : MonoBehaviour
{

    [SerializeField] 
    private float damage = 10f;


    public float speed = 5f;
    private Vector2 direction;

    [SerializeField] 
    private float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }


    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    
    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        RotateSprite();
    }

    void RotateSprite()
    {
        if (direction == Vector2.zero) return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 180f);
    
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Barrier"))
        {
            Destroy(gameObject);
            return;
        }
        
            if (!collision.CompareTag("Player"))
            return;

        var health = collision.GetComponent<PlayerHealthController>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }


}

