using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance { get; private set; }
    [field: SerializeField]
    internal GameObject EnemyObjectReference;
    internal static Queue<Transform> enemyPath;
    private Dictionary<GameObject,float> currentEnemiesAndTheirSpeeds;
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        RenewPath();
        currentEnemiesAndTheirSpeeds = new();
    }
    void LateUpdate()
    {
        CheckAndStopEnemiesForPause();
    }
    public void OnEnemyDeath(EnemyHandler enemy)
    {
        if (enemy != null && enemy.CurrentHealth <= 0)
        {
            ResourceManager.instance.MoneyGainFromEnemy(enemy.enemyObject);
            Destroy(enemy.gameObject, 0);
        }
    }
    public void SpawnEnemy(EnemySO enemyToSpawn)
    {
        if (enemyToSpawn == null || ResourceManager.instance.currentLives.GetValue() <= 0) return;
        if (instance != null && instance != this)
        {
            instance.SpawnEnemy(enemyToSpawn);
            return;
        }
        if (enemyPath == null || enemyPath.Count <= 0)
        {
            instance.RenewPath();
        }
        Queue<Transform> newEnemyPath = new(enemyPath);
        Transform startingPoint = newEnemyPath.Dequeue();
        GameObject newEnemy = Instantiate(EnemyObjectReference, startingPoint.position, Quaternion.identity);
        if (newEnemy.TryGetComponent<EnemyHandler>(out EnemyHandler enemyHandler))
        {
            enemyHandler.SetUp(enemyToSpawn, newEnemyPath);
        }
        else
        {
            Debug.LogWarning("Warning, enemy " + enemyToSpawn.EnemyName + " Did not had enemyHandler. Despawning to prevent bugs");
            Destroy(newEnemy);
        }
        currentEnemiesAndTheirSpeeds.Add(newEnemy, enemyToSpawn.EnemySpeed);
    }
    protected internal void RenewPath()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                instance.RenewPath();
                return;
            }
            enemyPath = EnemyPathingManager.GetPath();
        }
    }
    private void CheckAndStopEnemiesForPause()
    {
        if (GameManager.instance.paused)
        {
            foreach (KeyValuePair<GameObject, float> pair in currentEnemiesAndTheirSpeeds)
            {
                if (pair.Key == null)
                {
                    continue;
                }
                if (pair.Key.TryGetComponent(out NavMeshAgent agent))
                {
                    agent.speed = 0;
                }
            }
        }
        else
        {
            foreach (KeyValuePair<GameObject, float> pair in currentEnemiesAndTheirSpeeds)
            {
                if (pair.Key == null)
                {
                    continue;
                }
                if (pair.Key.TryGetComponent(out NavMeshAgent agent))
                {
                    agent.speed = pair.Value;
                }
            }            
        }
    }
}
