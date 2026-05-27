using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour
{

    public int baseCoinValue = 1;
     public Animator animator;

    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerWallet wallet = collision.GetComponent<PlayerWallet>();
            
            if (wallet != null)
            {
                wallet.AddCoins(baseCoinValue);
               
                StartCoroutine(PickupRoutine());
            }

        }
    }

    private IEnumerator PickupRoutine()
    {
        if (animator != null)
        {
            animator.SetBool("IsPickedUp", true);
        }

        // Wait for a fraction of a second (adjust this 0.5f to match exactly how long your animation is!)
        yield return new WaitForSeconds(1f);

        // NOW destroy the coin
        Destroy(gameObject);
    }
}
