using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Transform itemSpawnPoint;

    public float spawnRadius = 1.5f;
    
    public void BuyItem(GameObject itemPrefab)
    {
        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        Vector3 finalDropPosition = itemSpawnPoint.position + new Vector3(randomOffset.x, randomOffset.y, 0f);
        Instantiate(itemPrefab, finalDropPosition, Quaternion.identity);
    }

    public void CloseShop()
    {
        gameObject.SetActive(false); 
        Time.timeScale = 1f;         
    }
}
