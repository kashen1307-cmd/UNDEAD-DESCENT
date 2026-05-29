using UnityEngine;
using UnityEngine.InputSystem;  
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    
    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    public Animator animator;
    public SpriteRenderer playerSprite;
    public InputActionReference dashInput; 
    private Rigidbody2D rb;
    public  bool isDashing = false;
    private bool canDash = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnDash(InputValue value)
    {
        Debug.Log("Dash input received");
        
        if (canDash && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        animator.SetTrigger("DashTrigger");
        
        Vector2 dashDirection = rb.linearVelocity.normalized;

        if (dashDirection == Vector2.zero) 
        {
            dashDirection = new Vector2(1, 0); 
        }

        float elapsedTime = 0f;
        float nextGhostTime = 0f;
        
        // This loop runs every single frame until the time runs out
        while (elapsedTime < dashDuration)
        {
            // Force the speed to stay at maximum the entire time!
            rb.linearVelocity = dashDirection * dashForce;
            
            if (elapsedTime >= nextGhostTime)
        {
            SpawnGhost();
            nextGhostTime = elapsedTime + 0.05f; // Drop a new shadow clone every 0.05 seconds
        }
            // Add the time that just passed to our timer
            elapsedTime += Time.deltaTime;
            
            // Tell Unity to wait for the next frame before looping again
            yield return null; 
        }

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;

    }
    
    private void SpawnGhost()
    {
        GameObject ghost = new GameObject("DashGhost");
        ghost.transform.position = transform.position;
        ghost.transform.localScale = transform.localScale; // Flips the ghost if you are facing left!

        // 2. Slap a SpriteRenderer on it and copy the player's exact frame
        SpriteRenderer ghostSprite = ghost.AddComponent<SpriteRenderer>();
        ghostSprite.sprite = playerSprite.sprite; 
    
        // 3. Make it half-transparent and push it behind the player
        ghostSprite.color = new Color(1f, 1f, 1f, 0.4f); 
        ghostSprite.sortingOrder = playerSprite.sortingOrder - 1; 

        // 4. Force Unity to delete the ghost after half a second
        Destroy(ghost, 0.5f);
    }
    
}
