using UnityEngine;
using TMPro;

public class EnemyCounterUI : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI counterText;
    
    public static int enemiesAlive = 0;

    void Update()
    {
        counterText.text = "Enemies Remaining: " + EnemyCounterUI.enemiesAlive;
      
    }
}