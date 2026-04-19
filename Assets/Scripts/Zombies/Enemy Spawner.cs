using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private float _minimumSpawnTime;

    [SerializeField]
    private float _maximumSpawnTime;

    [SerializeField]
    private float _timeUntilSpawn;

    [SerializeField] 
    private int _maxTotalEnemies = 15;

    

    private int _totalSpawned = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;

        if (_timeUntilSpawn <= 0 && _totalSpawned < _maxTotalEnemies)
        {
           GameObject enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);

            EnemyCounterUI.enemiesAlive++;


            _totalSpawned++;

            // Tell enemy who spawned it
            _enemyPrefab.GetComponent<EnemyMovement>().SetSpawner(this);

            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }

}
