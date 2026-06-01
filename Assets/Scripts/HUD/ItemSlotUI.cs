using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
   public Image iconDisplay;
    public TextMeshProUGUI countText;
    
    public void Setup(ItemSO item, int amount)
    {
        
        iconDisplay.sprite = item.icon;
        iconDisplay.preserveAspect = true;
        
        if (amount > 1)
        {
            countText.text = amount.ToString();
            countText.gameObject.SetActive(true);
        }
        else
        {
            countText.gameObject.SetActive(false);
        }
    }
}
