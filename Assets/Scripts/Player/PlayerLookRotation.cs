using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookRotation : MonoBehaviour
{
    
    [SerializeField] private Camera mainCamera;
    
    private Vector2 mousePosition;

    InputSystem_Actions inputSystem;

    private float rotationOffset = -90f; 

    void Awake()
    {
        mainCamera = Camera.main;
        
        
    }

    

    private void OnLook(InputValue inputValue)
    {
         mousePosition = inputValue.Get<Vector2>(); 
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
    }

    void RotatePlayer()
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));

        Vector3 rotateDirection = (worldPosition - transform.position).normalized;   
        rotateDirection.z = 0; 

        float angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg + rotationOffset;  
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));    
    }
}
