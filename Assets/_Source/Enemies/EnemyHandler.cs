using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyHandler : MonoBehaviour, IDamagable
{
    public double CurrentHealth { get; private set; }
    public double MaxHealth { get; private set; }
    [field: SerializeField]
    private EnemySO enemyObject;
    [field: SerializeField]
    private UnityEvent<EnemyHandler> onEnemyDeath;
    private Queue<Transform> path;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();    
    }
    void Update()
    {
        if (agent != null)
        {
            if ((agent.isStopped||agent.destination == null) &&path.Count >= 1)
            {
                Transform next = path.Dequeue();
                agent.SetDestination(next.position);
            }
        }
        if (CurrentHealth <= 0)
        {
            agent.speed = 0;
            onEnemyDeath.Invoke(this);
        }
    }
    void IDamagable.Damage(double Damage)
    {
        CurrentHealth = Math.Clamp(CurrentHealth - Damage, 0, CurrentHealth);
    }
 /*   internal void SetUp(EnemySO enemy, List<Transform> newPath)
    {
        if (enemy == null || enemyObject != null || newPath == null) return;
        agent = GetComponent<NavMeshAgent>();
        agent.baseOffset = enemy.EnemyAgentHeightOffset;
        enemyObject = enemy;
        CurrentHealth = enemy.EnemyMaxHealth;
        if (enemy.EnemyModel != null && TryGetComponent<MeshFilter>(out MeshFilter filter))
        {
            filter.mesh = enemy.EnemyModel;
        }
        path = new();
        path.Union(newPath);
    }*/
    internal void SetUp(EnemySO enemy, Queue<Transform> newPath)
    {
        if (enemy == null || enemyObject != null || newPath == null) return;
        agent = GetComponent<NavMeshAgent>();
        agent.baseOffset = enemy.EnemyAgentHeightOffset;
        enemyObject = enemy;
        CurrentHealth = enemy.EnemyMaxHealth;
        if (enemy.EnemyModel != null && TryGetComponent<MeshFilter>(out MeshFilter filter))
        {
            filter.mesh = enemy.EnemyModel;
        }
        path = newPath;
    }
}
