using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    
    public GameObject slotPrefab;
    public Transform gridContainer;
    
    void OnEnable()
    {
        RefreshInventoryDisplay();
    }
    
    public void RefreshInventoryDisplay()
    {
        // 1. Destroy any old icons so we don't accidentally double-draw them
        foreach (Transform child in gridContainer)
        {
            Destroy(child.gameObject);
        }

        // 2. Ask the GameManager for the master list
        if (GameManager.instance != null)
        {
            Debug.Log("UI thinks the player has this many items: " + GameManager.instance.playerInventory.Count);
            
            foreach (GameManager.InventorySlot slot in GameManager.instance.playerInventory)
            {
                // 3. Spawn a new UI prefab for every item in the list
                GameObject newUIObj = Instantiate(slotPrefab, gridContainer);
                
                // 4. Send the data to the script we made earlier
                ItemSlotUI slotUI = newUIObj.GetComponent<ItemSlotUI>();
                slotUI.Setup(slot.itemData, slot.count);
            }
        }
    }
}
