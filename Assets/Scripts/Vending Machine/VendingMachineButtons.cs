using UnityEngine;
using TMPro;

public class VendingMachineButtons : MonoBehaviour
{
    
    public ItemSO itemData;
    public int manualPrice = 50;           // Type the price right here!
    public GameObject prefabToDispense;
    public TextMeshProUGUI priceText;
    public ShopManager shopManager;
    private int finalPrice;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (itemData != null)
        {
            // The slot is full! Steal the master price from the ItemSO.
            finalPrice = itemData.itemCost; 
        }
        else
        {
            // The slot is empty! Fall back to the manual price typed in the box.
            finalPrice = manualPrice; 
        }

        // 2. Update the text on the screen to match
        if (priceText != null)
        {
            priceText.text = "$" + finalPrice.ToString();
        }
    }

    public void OnPurchaseClicked()
    {
        if (shopManager != null && prefabToDispense != null)
        {
            // Tell the vending machine the price and what to spawn
            shopManager.BuyItem(finalPrice, prefabToDispense);
        }
    }
}
