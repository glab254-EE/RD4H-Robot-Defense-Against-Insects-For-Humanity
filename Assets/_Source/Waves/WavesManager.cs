using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [field: SerializeField]
    internal List<EnemyWave> waves;
    private float time;
    private bool started;
    void Start()
    {

    }
    void Update()
    {

    }
}
[System.Serializable]
public class EnemyWave
{
    [field: SerializeField]
    public List<EnemySO> Enemies { get; protected set; }
    [field: SerializeField]
    public List<float> TimeBetweenWaves { get; protected set; }
    [field: SerializeField]
    public float NextWaveTime { get; protected set; }
}