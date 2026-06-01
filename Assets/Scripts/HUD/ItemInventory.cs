using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    
    public GameObject slotPrefab;
    public Transform gridContainer;
    private bool hasRefreshed = false;
    
    void OnEnable()
    {
        hasRefreshed = false;
    }

    private void Update()
    {
        if (!hasRefreshed && GameManager.instance != null)
        {
            RefreshInventoryDisplay();
            
            
            hasRefreshed = true; 
        }
    }
    
    public void RefreshInventoryDisplay()
    {
        
        foreach (Transform child in gridContainer)
        {
            Destroy(child.gameObject);
        }

        
        if (GameManager.instance != null)
        {
            Debug.Log("UI thinks the player has this many items: " + GameManager.instance.playerInventory.Count);
            
            foreach (GameManager.InventorySlot slot in GameManager.instance.playerInventory)
            {
                
                GameObject newUIObj = Instantiate(slotPrefab, gridContainer);
                
                
                ItemSlotUI slotUI = newUIObj.GetComponent<ItemSlotUI>();
                slotUI.Setup(slot.itemData, slot.count);
            }
        }
    }
}
