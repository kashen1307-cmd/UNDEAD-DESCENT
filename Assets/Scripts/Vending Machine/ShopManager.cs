using UnityEngine;

public class ShopManager : MonoBehaviour
{

    
    public Transform itemSpawnPoint;

    public float spawnRadius = 1.5f;
    
    public void BuyItem(int price, GameObject itemPrefab)
    {
        // 1. Find the player's wallet
        PlayerWallet playerWallet = FindFirstObjectByType<PlayerWallet>();

        if (playerWallet != null)
        {
            // 2. Try to spend the money
            if (playerWallet.SpendCoins(price))
            {
                // 3. Purchase successful! Spit the item onto the floor.
                Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
                Vector3 finalDropPosition = itemSpawnPoint.position + new Vector3(randomOffset.x, randomOffset.y, 0f);
                Instantiate(itemPrefab, finalDropPosition, Quaternion.identity);

                Debug.Log("Item dispensed!");
            }
            else
            {
                Debug.Log("Not enough coins!");
            }
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
