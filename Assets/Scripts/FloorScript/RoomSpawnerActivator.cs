

using UnityEngine;

public class RoomSpawnerActivator : MonoBehaviour
{
    [SerializeField] 
    private GameObject EnemySpawnerFloor2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EnemySpawnerFloor2.SetActive(true);
        }
    }
}
