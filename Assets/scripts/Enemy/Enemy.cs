using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    public float maxHP = 100f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public float damage = 10f;

    public event Action OnDeath;

    private float currentHP;
    private float lastAttackTime;
    private Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        currentHP = maxHP;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance > attackRange)
            {
                agent.SetDestination(target.position);
            }
            else if (Time.time > lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Attack()
    {
        Debug.Log($"{name} атакует {target.name}");
        // Реализуй получение урона игроком, если нужно
    }

    void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
