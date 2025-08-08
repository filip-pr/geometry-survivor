using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform enemyTarget;
    [SerializeField] private float minSpawnDistance = 5f;
    [SerializeField] private float maxSpawnDistance = 10f;

    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private int spawnPoints = 0;

    private void Start()
    {
        StartCoroutine(GatherSpawnPoints());
    }

    private IEnumerator GatherSpawnPoints()
    {
        while (true)
        {
            spawnPoints += 1;
            yield return new WaitForSeconds(1f);
        }
    }

    private void SpawnEnemy()
    {

        Vector2 spawnPosition = Random.insideUnitCircle.normalized * Random.Range(minSpawnDistance, maxSpawnDistance);
        spawnPosition += (Vector2)enemyTarget.position;
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.Target = enemyTarget;
        }
    }

    private void Update()
    {
        if (spawnPoints > 2)
        {
            SpawnEnemy();
            spawnPoints -= 2;
        }
    }
}
