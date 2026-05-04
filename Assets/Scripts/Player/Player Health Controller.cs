
using UnityEngine.SceneManagement;
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

    public float CurrentHealth => _currentHealth;

    public bool isNewRun = true;

    public void ResetRun()
    {
        isNewRun = true;
        _currentHealth = _maximumHealth;
    }


    private void Start()
    {
        if (GameManager.instance == null) return;

        _maximumHealth = GameManager.instance.maxHealth;
        _currentHealth = GameManager.instance.playerHealth;
    }



    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth <= 0 || IsInvinvcible)
            return;

        _currentHealth -= damageAmount;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;

            GameManager.instance.playerHealth = 0;

            OnHealthChanged.Invoke();
            OnDied.Invoke();

            SceneManager.LoadScene("DeathScreen");
            return;
        }

        OnHealthChanged.Invoke();
        OnDamaged.Invoke();

        GameManager.instance.playerHealth = _currentHealth;
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
