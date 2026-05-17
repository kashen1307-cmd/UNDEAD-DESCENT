using System.Collections;
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

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        animator.SetTrigger("Hit");
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
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        EnemyMovement movement = GetComponent<EnemyMovement>();

        if (movement != null)
            movement.isDead = true;

        animator.SetTrigger("Die");

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }


}