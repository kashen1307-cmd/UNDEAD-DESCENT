using UnityEngine;
using TMPro;

public class EnemyCounterUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI counterText;

    void Update()
    {
        int enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;

        counterText.text = "Enemies Remaining: " + enemiesAlive;
    }
}