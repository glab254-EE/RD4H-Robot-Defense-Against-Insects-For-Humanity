using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [field: SerializeField]
    internal GameObject EnemyObjectReference;
    public static EnemyManager instance { get; private set; }
    internal static Queue<Transform> enemyPath;
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        RenewPath();
    }
    public void OnEnemyDeath(EnemyHandler enemy)
    {
        if (enemy != null && enemy.CurrentHealth <= 0)
        {
            ResourceManager.instance.MoneyGainFromEnemy(enemy.enemyObject);
            Destroy(gameObject, 0);  
        }
    }
    public void SpawnEnemy(EnemySO enemyToSpawn)
    {
        if (enemyToSpawn == null) return;
        if (instance != null&&instance != this)
        {
            instance.SpawnEnemy(enemyToSpawn);
            return;
        }
        if (enemyPath == null || enemyPath.Count <= 0)
        {
            instance.RenewPath();
        }
        Queue<Transform> newEnemyPath = enemyPath;
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
}
