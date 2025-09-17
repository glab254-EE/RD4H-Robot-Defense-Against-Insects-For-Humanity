using Unity.Mathematics;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [field: SerializeField]
    public SDouble Resource { get; private set; }
    [field: SerializeField]
    public SInt MaxLives { get; set; }
    internal static ResourceManager instance;
    private SInt currentLives;
    void Start()
    {
        currentLives = MaxLives;
        if (instance != null) // in case of loaded new level
        {
            instance.Resource = new();
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    internal bool CanBuy(SDouble ammount)
    {
        if (Resource < ammount) return false;
        return true;
    }
    internal bool TryBuy(SDouble ammount)
    {
        if (Resource == 0 || Resource < ammount) return false;
        Resource -= ammount;
        Debug.Log(Resource);
        return true;
    }
    internal void Gain(SDouble ammount)
    {
        Resource += math.abs(ammount.GetValue());
    }
    internal void MoneyGainFromEnemy(EnemySO enemy)
    {
        if (enemy == null || enemy.EnemyCost <= 0) return;
        Resource += enemy.EnemyCost;
        Debug.Log(Resource);
    }
    internal void OnEnemyFinishPath(EnemySO enemy)
    {
        if (enemy == null || currentLives <= 0 || enemy.EnemyDamage <= 0) return;
        currentLives = new(math.clamp(currentLives.GetValue() - enemy.EnemyDamage.GetValue(), 0, MaxLives.GetValue()));
        if (currentLives <= 0)
        { // temporary, ignore it
            Debug.Log("GG, you died, L bozo, get gud, ez, lkjthr,fi056c,.v5-0yvji8,697yu!");
        }
    }
}
