using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private EnemyMovement movement;



    void Start()
    {
        currentHealth = maxHealth;
    }

     public void SetHealth(int newHealth)
    {
        maxHealth = newHealth;
        currentHealth = newHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hit");
        StartCoroutine(HitStun());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private System.Collections.IEnumerator HitStun()
    {
        movement.isStunned = true;

        animator.SetTrigger("Hit");

        yield return new WaitForSeconds(0.4f);

        movement.isStunned = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    
}