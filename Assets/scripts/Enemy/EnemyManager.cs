using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Префаб врага")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Точки спавна")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Настройки волны")]
    [SerializeField] private int enemiesPerSpawn = 1;
    [SerializeField] private float spawnDelay = 5f;

    void Start()
    {
        StartCoroutine(SpawnEnemiesLoop());
    }

    private IEnumerator SpawnEnemiesLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                Vector3 offset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                Instantiate(enemyPrefab, spawnPoint.position + offset, Quaternion.identity);
            }
        }
    }
}
