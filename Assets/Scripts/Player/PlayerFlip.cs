using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    
    public SpriteRenderer playerSpriteRenderer;

    public Transform weaponSocket;

    public float rotateRadius = 0.6f;

    public Vector3 rotateOffset = new Vector3(0f, 0.2f, 0f);

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; 

        if (mousePos.x < transform.position.x) playerSpriteRenderer.flipX = true; 
        else if (mousePos.x > transform.position.x) playerSpriteRenderer.flipX = false; 

        if (weaponSocket != null)
        {
            Vector3 centerPoint = transform.position + rotateOffset;
            Vector3 aimDirection = (mousePos - centerPoint).normalized;
            
            // --- THE NEW CODE ---
            // 1. Default to the player's radius setting
            float currentRadius = rotateRadius; 

            // 2. Ask the current gun if it has a custom radius
            WeaponDirectionManager gunScript = weaponSocket.GetComponentInChildren<WeaponDirectionManager>();
            if (gunScript != null)
            {
                currentRadius = gunScript.weaponOrbitRadius; // Use the gun's radius instead!
            }

            // 3. Move the socket using the correct radius
            Vector3 newPosition = centerPoint + (aimDirection * currentRadius);
            newPosition.z = weaponSocket.position.z; 
            
            weaponSocket.position = newPosition;
        }
    }
}
