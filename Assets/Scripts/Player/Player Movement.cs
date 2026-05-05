using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float _speed;

    [SerializeField]
    private Rigidbody2D _rigidbody;

    [SerializeField] 
    private AudioSource footstepAudio;

    [SerializeField] 
    private AudioClip footstepClip;

    [SerializeField] 
    private float stepInterval = 0.4f;

    private bool wasMoving = false;

    private float stepTimer;

    private bool isRunning = false;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 movementInputSmoothVelocity;

    [SerializeField] private Animator animator;

    public GameObject MainCamera;



    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    

    void Update()
    {
        animator.SetBool("IsRunning", _movementInput.magnitude > 0.1f);

        bool isMoving = _movementInput.magnitude > 0.1f;

        if (isMoving)
        {
            if (!footstepAudio.isPlaying)
            {
                footstepAudio.clip = footstepClip;
                footstepAudio.loop = true;
                footstepAudio.Play();
            }
        }
        else
        {
            if (footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }
        }

        wasMoving = isMoving;
    }
    private void FixedUpdate()
    {
        SetPlayerVelocity();
    }

    private void SetPlayerVelocity()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
                    _smoothedMovementInput,
                    _movementInput,
                    ref movementInputSmoothVelocity,
                    0.1f);

        _rigidbody.linearVelocity = _smoothedMovementInput * _speed; ;

        
    }




    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();

    }

    public void IncreaseSpeed(float speedAmount)
{
    _speed += speedAmount; 
    Debug.Log("Speed Increased! New Speed: " + _speed);
}
}
