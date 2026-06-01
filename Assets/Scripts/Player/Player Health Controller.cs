
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
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

    public SpriteRenderer playerSprite; 

    public Material whiteFlashMaterial;

    private Material defaultMaterial;

    public float invincibilityDuration = 1.5f; 

    public float blinkInterval = 0.1f; 

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

        if (playerSprite != null)
        {
            defaultMaterial = playerSprite.material; 
        }

        OnHealthChanged.Invoke();
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

        StartCoroutine(DamageFlashRoutine());
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

    private IEnumerator DamageFlashRoutine()
    {
        // 1. Lock the door so zombies can't hurt us again
        IsInvinvcible = true;

        playerSprite.material = whiteFlashMaterial;
        yield return new WaitForSeconds(0.15f);

        playerSprite.material = defaultMaterial;

        // 3. THE BLINKING LOOP (I-Frames)
        float elapsedTime = 0.1f;
        while (elapsedTime < invincibilityDuration)
        {
            // Drop alpha to 0 (invisible)
            playerSprite.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(blinkInterval);

            // Return alpha to 1 (visible)
            playerSprite.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(blinkInterval);

            // Add the time we just spent waiting to our timer
            elapsedTime += (blinkInterval * 2);
        }

        // 4. THE SAFETY NET: Force the player back to perfectly visible and normal color
        playerSprite.color = Color.white; 
        
        // 5. Unlock the door so we can take damage again
        IsInvinvcible = false;
    }
}
