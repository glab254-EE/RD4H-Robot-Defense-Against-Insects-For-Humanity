using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyHandler : MonoBehaviour, IDamagable
{
    public double CurrentHealth { get; private set; }
    public double MaxHealth { get; private set; }
    [field: SerializeField]
    internal EnemySO enemyObject { get; private set; }
    private Queue<Transform> path;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (enemyObject != null)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.baseOffset = enemyObject.EnemyAgentHeightOffset;
            CurrentHealth = enemyObject.EnemyMaxHealth;
        }
    }
    void Update()
    {
        if (agent != null)
        {
            Debug.Log(agent.destination);
            if (path != null && (agent.destination == null || Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance))
            {
                Debug.Log(path);
                if (path.Count >= 1)
                {
                    Transform next = path.Dequeue();
                    if (!agent.SetDestination(next.position))
                    {
                        agent.destination = next.position;
                    }
                    Debug.Log(agent.destination);
                }
                else
                {
                    if (CurrentHealth > 0 && agent.speed > 0)
                    {
                        agent.speed = 0;
                        ResourceManager.instance.OnEnemyFinishPath(enemyObject);
                        Destroy(gameObject);
                    }
                }
            }
        }
        else
        {
            agent = GetComponent<NavMeshAgent>();
        }
        if (CurrentHealth <= 0 && agent.speed > 0)
        {
            agent.isStopped = true;
            agent.speed = 0;
            EnemyManager.instance.OnEnemyDeath(this);
        }
    }
    void IDamagable.Damage(double Damage)
    {
        CurrentHealth = Math.Clamp(CurrentHealth - Damage, 0, CurrentHealth);
    }
    internal void SetUp(EnemySO enemy, Queue<Transform> newPath)
    {
        if (enemy == null || newPath == null)
        {
            Debug.LogWarning("Failed to set-up; enemy == null => " + enemy == null + "OR SMTH WITH PATH");
            return;
        }
        if (enemy.EnemyVisualModel != null)
        {
            GameObject Visual = Instantiate(enemy.EnemyVisualModel, transform);
            Visual.transform.localPosition = enemy.EnemyVisualOffset != null ? enemy.EnemyVisualOffset : Vector3.zero;
        }
        if (enemy.EnemySize != null && enemy.EnemySize != Vector3.zero)
        {
            transform.localScale = enemy.EnemySize;
        }
        agent = GetComponent<NavMeshAgent>();
        agent.baseOffset = enemy.EnemyAgentHeightOffset;
        agent.speed = enemy.EnemySpeed;
        enemyObject = enemy;
        CurrentHealth = enemy.EnemyMaxHealth;
        path = newPath;
    }
}
