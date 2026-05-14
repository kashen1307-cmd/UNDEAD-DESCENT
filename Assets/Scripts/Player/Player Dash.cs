using UnityEngine;
using UnityEngine.InputSystem;  
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    
    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    
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

        Vector2 dashDirection = rb.linearVelocity.normalized;

        float elapsedTime = 0f;
        
        // This loop runs every single frame until the time runs out
        while (elapsedTime < dashDuration)
        {
            // Force the speed to stay at maximum the entire time!
            rb.linearVelocity = dashDirection * dashForce;
            
            // Add the time that just passed to our timer
            elapsedTime += Time.deltaTime;
            
            // Tell Unity to wait for the next frame before looping again
            yield return null; 
        }

        isDashing = false;

       yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
    
    
}
