

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private float _timeToWaitBeforeExit;

    public float playerHealth;

    public float maxHealth = 100f;
    public int currentCoins = 0;

    public GameObject defaultPistolPrefab;

    public GameObject[] savedWeaponPrefabs = new GameObject[2];

    public GameObject[] savedDropPrefabs = new GameObject[2];

    public void ResetRun()
    {
        playerHealth = maxHealth;

        currentCoins = 0;

        savedWeaponPrefabs = new GameObject[2];
        savedDropPrefabs = new GameObject[2];
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
