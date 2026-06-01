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
        
        if (instance == null) instance = this; 
        
        
        gameObject.SetActive(false); 
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)Input.mousePosition + tooltipOffset;
        
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.unscaledDeltaTime * 50f);
       
    }


    public void ShowTooltip(string itemName, string itemDesc)
    {
        gameObject.SetActive(true);
        
       
        nameText.text = itemName;
        descriptionText.text = itemDesc;

        
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    
}
