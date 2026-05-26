using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    public int baseCoinValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerWallet wallet = collision.GetComponent<PlayerWallet>();
            
            if (wallet != null)
            {
                wallet.AddCoins(baseCoinValue);
                Destroy(gameObject); // Delete the coin from the floor
            }
        }
    }
}
