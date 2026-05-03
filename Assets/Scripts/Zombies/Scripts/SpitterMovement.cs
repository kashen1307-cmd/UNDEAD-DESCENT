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
    private SpriteRenderer spriteRenderer;

    private Transform player;
    private Rigidbody2D rb;

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
            rb.linearVelocity = dir * speed; // chase
        }
        else if (distance < minDistance)
        {
            rb.linearVelocity = -dir * speed; // back away
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // attack range
        }

        if (dir.x > 0.1f)
            spriteRenderer.flipX = false;
        else if (dir.x < -0.1f)
            spriteRenderer.flipX = true;
    }
}