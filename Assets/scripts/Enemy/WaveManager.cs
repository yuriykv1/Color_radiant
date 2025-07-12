using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner spawner;
    public int enemiesPerWave = 10;
    public float delayBetweenWaves = 5f;

    private int currentWave = 0;
    private List<GameObject> aliveEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(WaveRoutine());
    }

    IEnumerator WaveRoutine()
    {
        while (true)
        {
            currentWave++;
            Debug.Log($"Начинается волна {currentWave}");

            for (int i = 0; i < enemiesPerWave; i++)
            {
                GameObject enemy = spawner.SpawnEnemy(currentWave / 3);
                aliveEnemies.Add(enemy);

                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.OnDeath += () =>
                    {
                        aliveEnemies.Remove(enemy);
                        Debug.Log($"Осталось врагов: {aliveEnemies.Count}");
                    };
                }

                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitUntil(() => aliveEnemies.Count == 0);

            Debug.Log($"Волна {currentWave} завершена. Пауза {delayBetweenWaves} сек.");
            yield return new WaitForSeconds(delayBetweenWaves);
        }
    }
}
