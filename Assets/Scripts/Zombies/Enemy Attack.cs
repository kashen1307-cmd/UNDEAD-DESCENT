

using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damageAmount;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private Animator _animator;

    private bool _playerInRange;
    private float _lastAttackTime;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }

    private void Update()
    {
        if (_playerInRange && Time.time >= _lastAttackTime + _attackCooldown)
        {
            _animator.SetTrigger("Attack");
            _lastAttackTime = Time.time;
        }
    }

    // Called from animation event
    public void DealDamage()
    {
        if (_playerInRange)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                var health = player.GetComponent<PlayerHealthController>();
                if (health != null)
                {
                    health.TakeDamage(_damageAmount);
                }
            }
        }
    }
}

