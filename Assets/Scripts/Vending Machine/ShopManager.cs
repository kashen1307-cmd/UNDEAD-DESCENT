using UnityEngine;

public class ShopManager : MonoBehaviour
{

    
    public Transform itemSpawnPoint;

    public float spawnRadius = 1.5f;

    [SerializeField]
    private AudioSource vendingAudio;

    [SerializeField]
    private AudioClip dispenseClip;

    public void BuyItem(int price, GameObject itemPrefab)
    {
        
        PlayerWallet playerWallet = FindFirstObjectByType<PlayerWallet>();

        if (playerWallet != null)
        {
            
            if (playerWallet.SpendCoins(price))
            {
                
                Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
                Vector3 finalDropPosition = itemSpawnPoint.position + new Vector3(randomOffset.x, randomOffset.y, 0f);
                Instantiate(itemPrefab, finalDropPosition, Quaternion.identity);

                if (vendingAudio != null && dispenseClip != null)
                {
                    vendingAudio.PlayOneShot(dispenseClip);
                    CancelInvoke(nameof(StopDispenseSound));
                    Invoke(nameof(StopDispenseSound), 0.5f);
                }

                Debug.Log("Item dispensed!");
            }
            else
            {
                Debug.Log("Not enough coins!");
            }
        }

        void StopDispenseSound()
        {
            vendingAudio.Stop();
        }
    }
    public void CloseShop()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
