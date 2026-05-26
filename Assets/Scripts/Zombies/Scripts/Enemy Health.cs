using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public GameObject coinPrefab;
    public int minCoinsToDrop = 1;
    public int maxCoinsToDrop = 3;

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

        if (coinPrefab != null)
        {
            int coinsToDrop = Random.Range(minCoinsToDrop, maxCoinsToDrop + 1);

            for (int i = 0; i < coinsToDrop; i++)
            {
                Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
                Instantiate(coinPrefab, transform.position + randomOffset, Quaternion.identity);
            }
        }
        
        Destroy(gameObject);
    }


}