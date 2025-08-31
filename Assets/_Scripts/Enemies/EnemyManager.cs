using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [field: SerializeField] public Transform Target { get; set; }
    [field: SerializeField] public Canvas HealthBarCanvas { get; set; }

    [SerializeField] private float minSpawnDistance = 5f;
    [SerializeField] private float maxSpawnDistance = 10f;

    [SerializeField] private EnemySpawnData[] enemies;

    [SerializeField] private float nonWaveSpawnPoints = 0;
    [SerializeField] private float waveSpawnPoints = 0;
    [SerializeField] private float waveSpawnPointRatio = 0.7f;

    [SerializeField] private float spawnPointMultiplier = 10f;
    [SerializeField] private float waveInterval = 30f;

    [SerializeField] private float enemyHealthIncreaseRate = 5f;
    [SerializeField] private float enemyDamageIncreaseRate = 5f;

    private StatModifier enemyHealthModifier = new StatModifier();
    private StatModifier enemyDamageModifier = new StatModifier();

    private float runTime = 0f;
    private int wavesSpawned = 0;

    private float SpawnPointsGainRate => spawnPointMultiplier * (1 + runTime / 60f);

    private void SpawnEnemy(EnemySpawnData enemyData)
    {
        Vector2 spawnPosition = Random.insideUnitCircle.normalized * Random.Range(minSpawnDistance, maxSpawnDistance);
        spawnPosition += (Vector2)Target.position;
        GameObject enemy = Instantiate(enemyData.EnemyPrefab, spawnPosition, Quaternion.identity, transform);
        enemy.GetComponent<Health>().SetupHealthBar(HealthBarCanvas);
        enemy.GetComponent<EnemyController>().Target = Target;
    }

    private void TrySpawnWave()
    {
        if (wavesSpawned > runTime / waveInterval) return;
        enemyHealthModifier.IncreaseMultiplier(enemyHealthIncreaseRate / 100f);
        enemyDamageModifier.IncreaseMultiplier(enemyDamageIncreaseRate / 100f);
        EnemySpawnData mainEnemyType = WeightedRandom.Choose(enemies);
        while (waveSpawnPoints >= mainEnemyType.SpawnPointCost)
        {
            SpawnEnemy(mainEnemyType);
            waveSpawnPoints -= mainEnemyType.SpawnPointCost;
        }
        wavesSpawned++;
    }

    private void TrySpawnRandomEnemy()
    {
        EnemySpawnData spawnedEnemyData = WeightedRandom.Choose(enemies);
        if (spawnedEnemyData.SpawnPointCost > nonWaveSpawnPoints) return;
        SpawnEnemy(spawnedEnemyData);
        nonWaveSpawnPoints -= spawnedEnemyData.SpawnPointCost;
    }

    private void Update()
    {
        runTime += Time.deltaTime;
        waveSpawnPoints += SpawnPointsGainRate * Time.deltaTime * waveSpawnPointRatio;
        nonWaveSpawnPoints += SpawnPointsGainRate * Time.deltaTime * (1 - waveSpawnPointRatio);
        TrySpawnRandomEnemy();
        TrySpawnWave();
    }
}
