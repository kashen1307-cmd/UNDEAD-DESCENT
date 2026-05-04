using UnityEngine;

public class SpitterMovement : MonoBehaviour
{
    [SerializeField] 
    private float speed = 2f;

    [SerializeField] 
    private float minDistance = 4f;

    [SerializeField] 
    private float maxDistance = 8f;

    [SerializeField] 
    private Transform firePoint;
    

    [SerializeField] 
    private SpriteRenderer spriteRenderer;

    private Transform player;
    private Rigidbody2D rb;




    void UpdateFacing(Vector2 direction)
    {
        bool facingRight = direction.x > 0;

        spriteRenderer.flipX = !facingRight;

        firePoint.localPosition = new Vector3(
            Mathf.Abs(firePoint.localPosition.x) * (facingRight ? 1 : -1),
            firePoint.localPosition.y,
            firePoint.localPosition.z
        );
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > maxDistance)
        {
            rb.linearVelocity = dir * speed;
        }
        else if (distance < minDistance)
        {
            rb.linearVelocity = -dir * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        UpdateFacing(dir); 
    }
    
}