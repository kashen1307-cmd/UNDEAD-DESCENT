using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZone : MonoBehaviour
{
    public string EndOfGame = "EndOfGame";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length == 0)
            {
                SceneManager.LoadScene(EndOfGame);
            }
            else
            {
                Debug.Log("Kill all zombies first!");
            }
        }
    }
}
