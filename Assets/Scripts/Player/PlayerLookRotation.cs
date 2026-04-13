using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookRotation : MonoBehaviour
{
    
    [SerializeField] private Camera mainCamera;
    
    private Vector2 mousePosition;

    InputSystem_Actions inputSystem;

    void Awake()
    {
        mainCamera = Camera.main;
        
        /*inputSystem = new InputSystem_Actions();
        inputSystem.Player.Look.performed += OnLook;*/
    }

    /*private void OnEnable()
    {
        inputSystem.Player.Look.performed += OnLook;
        inputSystem.Enable();
    }

    private void OnDisable()
    {
        inputSystem.Player.Look.performed -= OnLook;
        inputSystem.Disable();
    }*/

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

        float angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;  // when angle is converted to radians, the values are (y,x) rather than (x,y)
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));    
    }
}
