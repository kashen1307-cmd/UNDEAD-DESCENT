
using System.Collections.Generic;
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
    [System.Serializable]    
    public class InventorySlot
    {
        public ItemSO itemData;
        public int count;
    }

    public List<InventorySlot> playerInventory = new List<InventorySlot>();

    public void AddItemToInventory(ItemSO newItem)
    {
        
        foreach (InventorySlot slot in playerInventory)
        {
            if (slot.itemData == newItem)
            {
                slot.count++; 
                return;
            }
        }

        
        InventorySlot newSlot = new InventorySlot();
        newSlot.itemData = newItem;
        newSlot.count = 1;
        playerInventory.Add(newSlot);
    }

    public void ResetRun()
    {
        playerHealth = maxHealth;

        currentCoins = 0;

        savedWeaponPrefabs = new GameObject[2];
        savedDropPrefabs = new GameObject[2];

        if (playerInventory != null)
        {
            playerInventory.Clear();
        }
    }

    private void Awake()
    {
        transform.SetParent(null); 

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else if (instance != this)
        {
            
            Destroy(gameObject);
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

    void OnLevelWasLoaded(int level)
    {
        
        
    }
}
