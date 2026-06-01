using UnityEngine;
using TMPro;

public class VendingMachineButtons : MonoBehaviour
{
    
    public ItemSO itemData;
    public int manualPrice = 50;           
    public GameObject prefabToDispense;
    public TextMeshProUGUI priceText;
    public ShopManager shopManager;
    private int finalPrice;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (itemData != null)
        {
            
            finalPrice = itemData.itemCost; 
        }
        else
        {
           
            finalPrice = manualPrice; 
        }

      
        if (priceText != null)
        {
            priceText.text = "$" + finalPrice.ToString();
        }
    }

    public void OnPurchaseClicked()
    {
        if (shopManager != null && prefabToDispense != null)
        {
            
            shopManager.BuyItem(finalPrice, prefabToDispense);
        }
    }
}
