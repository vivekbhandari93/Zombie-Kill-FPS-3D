using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 10f;
    
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;

    bool isProvoke = false;

    Animator animation;

    [SerializeField] float turnSpeed = 5f;

    EnemyHealth enemyHealth;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animation = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {

        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if(enemyHealth.IsDead()) 
        {
            enabled = false;
            navMeshAgent.enabled = false;
        }
        else if (isProvoke)
        {
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange)
        {
            isProvoke = true;
            
        }
    }

    public void OnDamageTaken()
    {
        isProvoke = true;
    }

    private void EngageTarget()
    {
        if (distanceToTarget > navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        else if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }
    
    private void ChaseTarget()
    {
        animation.SetBool("attack", false);
        animation.SetTrigger("walk");

        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        FaceTarget();
        animation.SetBool("attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
