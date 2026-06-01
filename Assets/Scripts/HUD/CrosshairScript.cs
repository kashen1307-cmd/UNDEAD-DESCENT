using UnityEngine;

public class CrosshairScript : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false; 
        
    }
    
    
    void Awake()
    {
         
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; 
            transform.position = mousePos;
        }
    }
    void OnDisable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
