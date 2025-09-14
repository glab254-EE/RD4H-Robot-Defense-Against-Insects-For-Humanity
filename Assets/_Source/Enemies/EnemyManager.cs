using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [field: SerializeField]
    private static GameObject EnemyObjectReference;
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
    }
    public static void OnEnemyDeath(EnemyHandler enemy)
    {
        if (enemy != null && enemy.CurrentHealth <= 0)
        {
            // TODO: ADD REWARDING FOR THIS.    
        }
    }
    public static void SpawnEnemy(EnemySO enemyToSpawn)
    {
        if (enemyToSpawn == null) return;
        if (enemyPath == null || enemyPath.Count <= 0)
        {
            instance.RenewPath();
        }
        else
        {
            Transform startingPoint = enemyPath.Peek();
            GameObject newEnemy = Instantiate(EnemyObjectReference, startingPoint.position, Quaternion.identity);
            if (newEnemy.TryGetComponent<EnemyHandler>(out EnemyHandler enemyHandler))
            {
                enemyHandler.SetUp(enemyToSpawn, enemyPath);
            }
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
