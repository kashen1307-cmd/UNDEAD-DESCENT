

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    

    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private float _changeDirectionCooldown;
    private EnemySpawner _spawner;

    



    public void SetSpawner(EnemySpawner spawner)
    {
        _spawner = spawner;
    }

    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _targetDirection = transform.up;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        SetVelocity();
        
    }


    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange();
        HandlePlayerTargeting();
        
    }


    private void PickNewRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        _targetDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
    }

    private void HandleRandomDirectionChange()
    {
        _changeDirectionCooldown -= Time.deltaTime;

        if (_changeDirectionCooldown <= 0)
        {
            PickNewRandomDirection();
            _changeDirectionCooldown = Random.Range(0.5f, 2f); // shorter = more natural
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only wander if NOT chasing player
        if (!_playerAwarenessController.AwareOfPlayer)
        {
            PickNewRandomDirection();
        }
    }


    private void HandlePlayerTargeting()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
    }

    

    private void SetVelocity()
    {    
        _rigidbody.linearVelocity = _targetDirection.normalized * _speed;       
    }


    



}
