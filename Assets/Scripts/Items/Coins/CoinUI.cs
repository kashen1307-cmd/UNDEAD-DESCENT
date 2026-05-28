using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{

    public TextMeshProUGUI coinTextDisplay;
    void Start()
    {
        Invoke("UpdateCoinDisplay", 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCoinDisplay();
    }

    public void UpdateCoinDisplay()
    {
        if (GameManager.instance != null && coinTextDisplay != null)
        {
            coinTextDisplay.text = "$" + GameManager.instance.currentCoins.ToString();
        }
    }
}
