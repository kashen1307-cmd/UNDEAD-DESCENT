using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public ItemSO itemInSlot;

    public bool useManualText = false; 
    public string manualName;
    public string manualDescription;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
       if (useManualText)
        {
            
            if (!string.IsNullOrEmpty(manualName))
            {
                TooltipManager.instance.ShowTooltip(manualName, manualDescription);
            }
        }
        
        else if (itemInSlot != null)
        {
            TooltipManager.instance.ShowTooltip(itemInSlot.itemName, itemInSlot.itemDescription);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        TooltipManager.instance.HideTooltip();
    }
}
