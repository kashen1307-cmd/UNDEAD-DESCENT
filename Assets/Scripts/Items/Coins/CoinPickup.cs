using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour
{
    [SerializeField]
    private AudioSource pickupAudio;

    [SerializeField]
    private AudioClip coinPickupClip;

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

                if (pickupAudio != null &&
                    coinPickupClip != null)
                {
                    pickupAudio.clip = coinPickupClip;
                    pickupAudio.Play();

                    CancelInvoke(nameof(StopPickupSound));
                    Invoke(nameof(StopPickupSound), 0.5f);
                }

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

        
        yield return new WaitForSeconds(1f);

        
        Destroy(gameObject);
    }

    void StopPickupSound()
    {
        if (pickupAudio != null)
        {
            pickupAudio.Stop();
        }
    }
}
