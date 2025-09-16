using Unity.Mathematics;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [field: SerializeField]
    public double Resource { get; private set; }
    internal static ResourceManager instance;
    void Start()
    {
        if (instance != null) // in case of loaded new level
        {
            instance.Resource = 0;
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    internal bool CanBuy(double ammount)
    {
        if (Resource == 0 || Resource < ammount) return false;
        return true;
    }
    internal bool TryBuy(double ammount)
    {
        if (Resource == 0 || Resource < ammount) return false;
        Resource -= ammount;
        Debug.Log(Resource);
        return true;
    }
    internal void Gain(double ammount)
    {
        Resource += math.abs(ammount);
    }
    internal void MoneyGainFromEnemy(EnemySO enemy)
    {
        if (enemy == null || enemy.EnemyCost <= 0) return;
        Resource += enemy.EnemyCost;
        Debug.Log(Resource);
    }
}
