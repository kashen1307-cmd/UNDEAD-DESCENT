using UnityEngine;
using UnityEngine.UI;

public class WeaponHUD : MonoBehaviour
{
    
    public static WeaponHUD instance;

    public Image slot1;
    public Image slot2;

    public float slideSpeed = 20f;

    public Vector2 centerPosition = new Vector2(117.3f, -106.7f);
    public Vector2 leftOffscreen = new Vector2(-200f, -106.7f);
   public Vector2 rightOffscreen = new Vector2(400f, -106.7f);

    private Image activeSlot;
    private Image inactiveSlot; 
    
   private void Awake()
    {
        if (instance == null) instance = this;

       
        activeSlot = slot1;
        inactiveSlot = slot2;

        
        inactiveSlot.color = new Color(1f, 1f, 1f, 0f);
        inactiveSlot.rectTransform.anchoredPosition = rightOffscreen;
    }

    // Update is called once per frame
    void Update()
    {
        
        activeSlot.rectTransform.anchoredPosition = Vector2.Lerp(activeSlot.rectTransform.anchoredPosition, centerPosition, Time.unscaledDeltaTime * slideSpeed);
        activeSlot.color = Color.Lerp(activeSlot.color, Color.white, Time.unscaledDeltaTime * slideSpeed);

       
        inactiveSlot.rectTransform.anchoredPosition = Vector2.Lerp(inactiveSlot.rectTransform.anchoredPosition, leftOffscreen, Time.unscaledDeltaTime * slideSpeed);
        inactiveSlot.color = Color.Lerp(inactiveSlot.color, new Color(1f, 1f, 1f, 0f), Time.unscaledDeltaTime * slideSpeed);
    
    }

    public void SwapWeapon(Sprite newWeaponSprite)
    {
        Image previousActive = activeSlot;
        activeSlot = inactiveSlot;
        inactiveSlot = previousActive;

        activeSlot.sprite = newWeaponSprite;
        activeSlot.rectTransform.anchoredPosition = rightOffscreen;
        activeSlot.color = new Color(1f, 1f, 1f, 0f);
    }
}
