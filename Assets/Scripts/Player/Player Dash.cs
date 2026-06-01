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
        
        
        
        while (elapsedTime < dashDuration)
        {
            
            rb.linearVelocity = dashDirection * dashForce;
               
            
            elapsedTime += Time.deltaTime;
            
            
            yield return null; 
        }

        isDashing = false;

        DashUI dashUI = Object.FindFirstObjectByType<DashUI>();
        if (dashUI != null)
        {
            dashUI.StartCooldown(dashCooldown);
        }

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;

    }
    
    public void ReduceDashCooldown(float reductionAmount)
    {
        dashCooldown -= reductionAmount; 

       
        if (dashCooldown < 0.2f) 
        {
            dashCooldown = 0.2f; 
        }
        
        Debug.Log("Dash cooldown is now: " + dashCooldown); 
    }
    
}
