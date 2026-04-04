using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f; // Speed of the Player 
    public Rigidbody2D rb; // Reference for Rigidbody2D
    private Vector3 movement; // Stores input values
                              
    // Update is called once per frame
    void Update()
    {

        //Gets input from player
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Applies Movement
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}
