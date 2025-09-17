using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [field: SerializeField]
    internal List<EnemyWave> waves;
    private float remainingTimeForNextWave;
    private bool paused;
    private bool isSpawningWave;
    private int lastWaveIndex;
    void Start()
    {
        paused = false;
        isSpawningWave = false;
        lastWaveIndex = 0;
        remainingTimeForNextWave = 5;
    }
    void Update()
    {
        if (!paused && !isSpawningWave)
        {
            if (remainingTimeForNextWave <= 0)
            {
                StartCoroutine(SpawnWave());
            }
            else
            {
                remainingTimeForNextWave -= Time.deltaTime;
            }
        }
    }
    private IEnumerator SpawnWave()
    {
        if (waves == null || waves.Count <= lastWaveIndex || waves[lastWaveIndex] == null) yield break;
        EnemyWave wave = waves[lastWaveIndex];
        if (wave == null)
        {
            yield break;
        }
        isSpawningWave = true;
        if (wave.NextWaveTime > 0)
        {
            remainingTimeForNextWave = wave.NextWaveTime;
        }
        if (wave.Enemies != null && wave.Enemies.Count > 0)
        {
            for (int i = 0; i < wave.Enemies.Count; i++)
            {
                if (wave.Enemies[i] == null)
                {
                    Debug.LogWarning("No enemy found for wave.Enemies in index " + i);
                    continue;
                }
                float nextEnemySpawnTime = 1f;
                if (wave.TimeBetweenWaves != null && wave.TimeBetweenWaves.Count > i && wave.TimeBetweenWaves[i] >= 0)
                {
                    nextEnemySpawnTime = wave.TimeBetweenWaves[i];
                }
                EnemyManager.instance.SpawnEnemy(wave.Enemies[i]);
                yield return new WaitForSeconds(nextEnemySpawnTime);
            }
        }
        lastWaveIndex++;
        isSpawningWave = false;
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