using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent (typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public float attackDistance = 15f;        ///|PathFinding|
    NavMeshAgent Agent;                              ///|PathFinding|
    ThirdPersonCharacter thirdPersonCharacter;       ///|PathFinding|
    public Transform target;                         ///|PathFinding|
    GameObject player;                               ///|PathFinding|

    [SerializeField] float currentHealth = 100;      ///|HealthSystem|
    [SerializeField] float maxHealth = 100;          ///|HealthSystem|

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        player = FindObjectOfType<Player>().gameObject;
    }
    void Update()
    {
        PathFinding();
    }

    void PathFinding()
    {
        float distance_from_player = Vector3.Distance(player.transform.position, transform.position);

        if (distance_from_player < attackDistance)
            target = player.transform;
        else
            target = null;

        if (target)
            Agent.SetDestination(target.position);
        if (Agent.remainingDistance > Agent.stoppingDistance)
            thirdPersonCharacter.Move(Agent.desiredVelocity, false, false);
        else
            thirdPersonCharacter.Move(Vector3.zero, false, false);
    }

    public float get_healthAsPercentage
    {
        get { return currentHealth / maxHealth; } /// returns a value below 1.
    }


}
