using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    
    public static TooltipManager instance;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    
    public Vector2 tooltipOffset = new Vector2(25f, 25f);
    
    private void Awake()
    {
        // Set up the singleton so the vending machine slots can find this easily
        if (instance == null) instance = this; 
        
        // Hide the tooltip when the game starts
        gameObject.SetActive(false); 
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)Input.mousePosition + tooltipOffset;
        // 2. Add the "Juice" - Rapidly grow the tooltip to full size for a snappy pop-in
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.unscaledDeltaTime * 50f);
        //transform.localScale = Vector3.one;
    }


    public void ShowTooltip(string itemName, string itemDesc)
    {
        gameObject.SetActive(true);
        
        // Inject the text from your ItemSO
        nameText.text = itemName;
        descriptionText.text = itemDesc;

        // Reset the scale to 0 instantly so the Update loop can smoothly "pop" it in
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    
}
