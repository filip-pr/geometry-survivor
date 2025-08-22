using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [field: SerializeField] public Transform Target { get; set; }
    [field: SerializeField] public Canvas HealthBarCanvas { get; set; }

    [SerializeField] private float minSpawnDistance = 5f;
    [SerializeField] private float maxSpawnDistance = 10f;

    [SerializeField] private EnemySpawnData[] enemies;

    [SerializeField] private int spawnPoints = 0;

    private void Start()
    {
        StartCoroutine(GatherSpawnPoints());
    }

    private IEnumerator GatherSpawnPoints()
    {
        while (true)
        {
            spawnPoints += 2;
            yield return new WaitForSeconds(1f);
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition = Random.insideUnitCircle.normalized * Random.Range(minSpawnDistance, maxSpawnDistance);
        spawnPosition += (Vector2)Target.position;
        EnemySpawnData spawnedEnemyData = WeightedRandom.Choose(enemies);
        GameObject enemy = Instantiate(spawnedEnemyData.EnemyPrefab, spawnPosition, Quaternion.identity, transform);
        if (enemy.TryGetComponent<Health>(out var enemyHealth))
        {
            enemyHealth.SetupHealthBar(HealthBarCanvas);
        }
        if (enemy.TryGetComponent<EnemyController>(out var enemyController))
        {
            enemyController.Target = Target;
        }
        spawnPoints -= spawnedEnemyData.SpawnPointCost;
    }

    private void Update()
    {
        if (spawnPoints > 20)
        {
            SpawnEnemy();
            
        }
    }
}
