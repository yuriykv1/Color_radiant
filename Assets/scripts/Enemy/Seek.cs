using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seek : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        player = GameObject.Find("character").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}
