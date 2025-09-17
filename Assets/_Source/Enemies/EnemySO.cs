using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    [Header("Enemy visuals")]
    [field: SerializeField]
    public string EnemyName { get; private set; }
    [field:SerializeField]
    public GameObject EnemyVisualModel { get; private set; }
    [field:SerializeField]
    public Vector3 EnemySize { get; private set; }
    [field:SerializeField]
    public Vector3 EnemyVisualOffset { get; private set; }
    [Header("Enemy stats")]
    [field:SerializeField]
    public float EnemyAgentHeightOffset { get; private set; }
    [field:SerializeField]
    public float EnemySpeed { get; private set; }
    [field:SerializeField]
    public double EnemyMaxHealth { get; private set; }
    [field:SerializeField]
    public double EnemyCost { get; private set; }
    [field:SerializeField]
    public SInt EnemyDamage { get; private set; }
}
