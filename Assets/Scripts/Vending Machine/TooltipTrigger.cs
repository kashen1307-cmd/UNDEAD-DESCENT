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
            // If the name isn't completely blank, show the tooltip
            if (!string.IsNullOrEmpty(manualName))
            {
                TooltipManager.instance.ShowTooltip(manualName, manualDescription);
            }
        }
        // If the box is NOT checked, do the normal ItemSO setup
        else if (itemInSlot != null)
        {
            TooltipManager.instance.ShowTooltip(itemInSlot.itemName, itemInSlot.itemDescription);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // When the mouse slides off the icon, turn the tooltip off
        TooltipManager.instance.HideTooltip();
    }
}
