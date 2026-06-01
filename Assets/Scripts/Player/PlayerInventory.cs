using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemSO> items = new List<ItemSO>();

    public void AddItemToInventory(ItemSO newItem)
    {
        items.Add(newItem);
        
        
        
    }
}
