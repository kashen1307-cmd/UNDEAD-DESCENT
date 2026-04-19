using UnityEngine;

public class CrosshairScript : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false; 
        //Cursor.lockState = CursorLockMode.Confined;
    }
    
    
    void Awake()
    {
        //Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
             Vector2 mousecursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousecursorPosition; // Move the crosshair to the mouse position
        }
        
       
    }
}
