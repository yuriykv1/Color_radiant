using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject spawnPoint;
    private int i = 0;
    private float timeTillSpawn;
    private float spawnCooldown;
    void Update()
    {
        if (i < 10 && timeTillSpawn <=0)
        {
            timeTillSpawn = spawnCooldown;
            GameObject villain = Instantiate(enemy);
            villain.transform.position = spawnPoint.transform.position;
        }
        timeTillSpawn -= Time.deltaTime;
        i += 1;
    }
}
