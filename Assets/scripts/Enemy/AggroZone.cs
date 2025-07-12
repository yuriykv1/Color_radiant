using UnityEngine;

public class AggroZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Enemy enemy = GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.SetTarget(other.transform);
            }
        }
    }
}
