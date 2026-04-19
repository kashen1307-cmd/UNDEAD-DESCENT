

using UnityEngine;
using UnityEngine.Events;
public class PlayerHealthController : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maximumHealth;

    public float RemainingHealthPercantage
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
    }

    public bool IsInvinvcible { get; set; }

    public UnityEvent OnDied;

    public UnityEvent OnDamaged;

    public UnityEvent OnHealthChanged;

    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth == 0)
        {
            return;
        }

        if (IsInvinvcible)
        {
            return;
        }


        _currentHealth -= damageAmount;

        OnHealthChanged.Invoke();

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if (_currentHealth == 0)
        {
            OnDied.Invoke();
        }
        else
        {
            OnDamaged.Invoke();
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if ( _currentHealth == _maximumHealth)
        {
            return;
        }

        _currentHealth += amountToAdd;

        OnHealthChanged.Invoke();

        if (_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }
    }

    public void SmallItemHeal(float healAmount)
    {
        
        _currentHealth += healAmount;
        
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maximumHealth);
    
        OnHealthChanged?.Invoke();
        
        Debug.Log("Player Healed! Current Health is now: " + _currentHealth);
    }    
}
