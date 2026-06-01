using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    
    public int Coins = 0;
    public float coinMultiplier = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.instance != null)
        {
            Coins = GameManager.instance.currentCoins;
        }
    }

    public void AddCoins(int amount)
    {
        
        int totalToAdd = Mathf.RoundToInt(amount * coinMultiplier);
        Coins += totalToAdd;
        
        
        if (GameManager.instance != null) GameManager.instance.currentCoins = Coins;
        
        Debug.Log("Picked up coins! Total Wallet: " + Coins);
    }

    public bool SpendCoins(int cost)
    {
        if (Coins >= cost)
        {
            Coins -= cost;
            if (GameManager.instance != null) GameManager.instance.currentCoins = Coins;
            return true; // Purchase successful!
        }
        else
        {
            Debug.Log("Not enough coins!");
            return false; // Purchase failed!
        }
    }
}
