

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private float _timeToWaitBeforeExit;

    public GameObject savedWeaponPrefab;
    public GameObject savedDropPrefab;


    public float playerHealth;
    public float maxHealth = 100f;

    public void ResetRun()
    {
        playerHealth = maxHealth;
        savedWeaponPrefab = null;
        savedDropPrefab = null;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // NEW
        if (playerHealth <= 0)
        {
            playerHealth = maxHealth;
        }
    }

    public void OnPlayerDied()
    {
        Invoke(nameof(EndGame), _timeToWaitBeforeExit);
    }

    private void EndGame()
    {
        SceneManager.LoadScene("DeathScreen");
    }
}
