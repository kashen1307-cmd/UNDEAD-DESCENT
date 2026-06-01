using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] 
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private float _minimumSpawnTime;

    [SerializeField]
    private float _maximumSpawnTime;

    [SerializeField]
    private float _timeUntilSpawn;

    [SerializeField] 
    private int _maxTotalEnemies = 15;

    

    [SerializeField] 
    private int zombieHealth = 3;

   

    [SerializeField] 
    private int spitterHealth = 2;

    




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
            GameObject prefabToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            GameObject enemy = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

            EnemyHealth health = enemy.GetComponent<EnemyHealth>();

            // Normal zombie
            if (enemy.GetComponent<EnemyMovement>() != null)
            {
                health.SetHealth(zombieHealth);
            }

            // Spitter zombie
            if (enemy.GetComponent<SpitterMovement>() != null)
            {
                health.SetHealth(spitterHealth);
            }





            _totalSpawned++;

            
            var normal = enemy.GetComponent<EnemyMovement>();
            if (normal != null)
            {
                normal.SetSpawner(this);
            }

            var spitter = enemy.GetComponent<SpitterMovement>();
      
            SetTimeUntilSpawn();
        }
        

    }


    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }

    public bool HasEnemiesRemaining()
    {
        return _totalSpawned < _maxTotalEnemies;
    }

}
