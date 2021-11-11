using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{    
    // задаем дистануию "тригера" чтобы начать преследование
    [SerializeField] public float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

    NavMeshAgent navMeshAgent;
    // кэшируем дистанцию в большое число по умолчанию(если 0 то будет сразу тригериться)
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    EnemyHealth health;
    Transform target;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;

    }

    void Update()
    {
        if (health.IsDead())
        {
            
            enabled = false;
            navMeshAgent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
        }
        // считаем дистанцию(дистанция игрока минус дистанция моба)
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageTarget();
        }
        // если дистанция меньше,провоцируем моба
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }
    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
    }

    //задаем, куда надо идти мобу(наш трансформ)
    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    private void FaceTarget()
    {
        // Механизм для того чтобы моб смотрел на нас когда атакует(СЛОЖНО)
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    //рисуем радиус видимости или тригера 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
