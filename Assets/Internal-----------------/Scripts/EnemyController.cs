using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private Animator enemyAnim;
    [SerializeField] private Transform waypoints;
    private int currentWaypoint;

    public NavMeshAgent agent;

    private void Update()
    {
        if (agent.remainingDistance <= .1f)
        {
            //currentWaypoint = Random.Range(1,5);
            currentWaypoint++;
            if(currentWaypoint >= waypoints.childCount)
            {
                currentWaypoint = 0;
            }
            agent.SetDestination(waypoints.GetChild(currentWaypoint).position);
        }
    }

}
