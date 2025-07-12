using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;

    public GameObject SpawnEnemy(int level)
    {
        GameObject prefab = enemyPrefabs[Mathf.Min(level, enemyPrefabs.Length - 1)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        return enemy;
    }
}
