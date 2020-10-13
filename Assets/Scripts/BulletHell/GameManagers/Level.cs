using System.Collections;
using UnityEngine;

public class Level : MonoBehaviour
{
    EnemyManager enemyManager;
    public Wave[] waves;
    int currentWave;
    float timeRemainingUntilNextWave;
    bool wavesComplete = false;
    private void Start()
    {
        enemyManager = GameManager.instance.enemyManager;
        currentWave = 0;
        timeRemainingUntilNextWave = waves[0].timeToActive;
    }
    private void Update()
    {
        if (!wavesComplete)
        {
            timeRemainingUntilNextWave -= Time.deltaTime;
            if (timeRemainingUntilNextWave <= 0)
            {
                StartCoroutine(StartWave(waves[currentWave]));
                currentWave++;
                if (currentWave >= waves.Length)
                    WaveComplete();
                else
                    timeRemainingUntilNextWave = waves[currentWave].timeToActive;
            }
        }
    }

    IEnumerator StartWave(Wave waveToSpawn)
    {
        for (int i = 0; i < waveToSpawn.spawnPackage.numberToSpawn; i++)
        {
            enemyManager.SpawnEnemy(waveToSpawn.spawnPackage.enemy,waveToSpawn.spawnPackage.movementType , Vector2.up * 8);
            yield return new WaitForSeconds(waveToSpawn.spawnPackage.timeSpacing);
        }
    }
    // void StartWave(Wave waveToSpawn)
    // {
    //     enemyManager.SpawnSmallEnemy(Vector2.up*3);
    //     Debug.Log("Wave Started");
    // }
    void WaveComplete()
    {
        Debug.Log("Waves Complete");
        wavesComplete = true;
    }
    void UpdateWave(Wave toUpdate)
    {

    }

}