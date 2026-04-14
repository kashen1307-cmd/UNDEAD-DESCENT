using UnityEngine;

public class CrosshairScript : MonoBehaviour
{
    
    void Awake()
    {
        Cursor.visible = false; // Hide the default cursor
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousecursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousecursorPosition; // Move the crosshair to the mouse position
    }
}
