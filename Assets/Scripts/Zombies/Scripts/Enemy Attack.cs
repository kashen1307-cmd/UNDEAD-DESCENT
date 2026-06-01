using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damageAmount;

    [SerializeField]
    private float _attackCooldown = 1f;

    [SerializeField]
    private float _attackRange = 1.5f;

    [SerializeField]
    private Animator _animator;

    private float _lastAttackTime;

    private Transform _player;

    void Start()
    {
        GameObject playerObj =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            _player = playerObj.transform;
        }
    }

    private void Update()
    {
        if (_player == null)
            return;

        float distance =
            Vector2.Distance(
                transform.position,
                _player.position);

        if (distance <= _attackRange &&
            Time.time >=
            _lastAttackTime + _attackCooldown)
        {
            _animator.SetTrigger("Attack");
            _lastAttackTime = Time.time;
        }
    }

   
    public void DealDamage()
    {
        if (_player == null)
            return;

        float distance =
            Vector2.Distance(
                transform.position,
                _player.position);

        if (distance <= _attackRange)
        {
            PlayerHealthController health =
                _player.GetComponent<PlayerHealthController>();

            if (health != null)
            {
                health.TakeDamage(_damageAmount);
            }
        }
    }
}

