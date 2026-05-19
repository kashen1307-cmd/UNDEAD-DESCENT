using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemSO> items = new List<ItemSO>();

    public void AddItemToInventory(ItemSO newItem)
    {
        items.Add(newItem);
        Debug.Log("Picked up: " + newItem.name + ". Total items: " + items.Count);
        
        // (Later, this is exactly where we will tell the UI Grid to draw the icon!)
    }
}
