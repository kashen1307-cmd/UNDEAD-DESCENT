using UnityEngine;

public class SpitterAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackRange = 6f;
    [SerializeField] private float cooldown = 2f;
    [SerializeField] private Animator animator;

    private Transform player;
    private float lastAttackTime;

    void Awake()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            if (Time.time >= lastAttackTime + cooldown)
            {
                animator.SetTrigger("Attack");
                lastAttackTime = Time.time;
            }
        }
    }

    // called by animation event
    public void Shoot()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Vector2 dir = (player.position - transform.position).normalized;

        proj.GetComponent<SpitProjectile>().SetDirection(dir);
    }
}

