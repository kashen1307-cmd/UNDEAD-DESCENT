
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
        // Check if we already have this item
        foreach (InventorySlot slot in playerInventory)
        {
            if (slot.itemData == newItem)
            {
                slot.count++; // Stack it!
                return;
            }
        }

        // If we don't have it, add a brand new slot to the list
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
    }

    private void Awake()
    {
        transform.SetParent(null); 

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("🟢 THE KING IS CROWNED: Floor 1 Manager is active and surviving.");
        }
        else if (instance != this)
        {
            Debug.Log("🔴 ASSASSIN DEFEATED: Floor 2 Manager woke up and destroyed itself.");
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
        // This fires the exact millisecond the new scene finishes booting up
        Debug.Log("🚪 SCENE LOADED! The current GameManager has exactly " + playerInventory.Count + " items.");
    }
}
