using UnityEngine;

public class WeaponDirectionManager : MonoBehaviour
{
    
    public SpriteRenderer gunSpriteRenderer;

    public Transform firePoint;

    public Sprite[] angleSprites = new Sprite[8];    
    public float weaponOrbitRadius = 0.6f;
    public Vector2[] firePointPositions = new Vector2[8];

    public int sortingOrderFront = 5;
    public int sortingOrderBehind = -5;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 aimDirection = mousePos - transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        if (angle < 0) angle += 360f;

        int spriteIndex = Mathf.FloorToInt((angle + 22.5f) / 45f);
        if (spriteIndex >= 8) spriteIndex = 0;

        
        if (angleSprites.Length == 8 && angleSprites[spriteIndex] != null) 
        {
            gunSpriteRenderer.sprite = angleSprites[spriteIndex];
        }

       
        if (firePoint != null)
        {
           
            firePoint.rotation = Quaternion.Euler(0, 0, angle);

            
            if (firePointPositions.Length == 8)
            {
                firePoint.localPosition = firePointPositions[spriteIndex];
            }
        }
        
       
        if (spriteIndex == 1 || spriteIndex == 2 || spriteIndex == 3)
        {
            gunSpriteRenderer.sortingOrder = sortingOrderBehind;
        }
        else
        {
            gunSpriteRenderer.sortingOrder = sortingOrderFront;
        }

        transform.rotation = Quaternion.identity;
    }
}
