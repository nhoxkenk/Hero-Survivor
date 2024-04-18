using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public int maxEnemiesPerWave = 20;
    public Wave[] waves;
    public Wave[] initialWaves;
    private int nextWaveIndex;
    private int currentWaveIndex = 1;
    public Transform[] spawnPoints;
    public float timeBetweenWave = 5f;
    private float waveCountdown;
    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;
    public int NextWaveIndex => nextWaveIndex + 1;
    public float WaveCountdown => waveCountdown;
    public SpawnState State => state;

    private void Awake()
    {
        if(spawnPoints.Length == 0)
        {
            Debug.Log("No Spawn points");
        }
        waveCountdown = timeBetweenWave;
    }

    private void Update()
    {
        if(GameManager.Instance.state == GameState.Decide)
        {
            if (state == SpawnState.WAITING)
            {
                if (EnemyIsAlive())
                {
                    return;
                }
                WaveCompleted();
            }

            if (waveCountdown <= 0f)
            {
                if (state != SpawnState.SPAWNING)
                {
                    StartCoroutine(SpawnWave(waves[nextWaveIndex]));
                }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        } 
    }

    private void WaveCompleted()
    {
        Debug.Log("Wave Completed!");
        currentWaveIndex++;
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWave;
        if (nextWaveIndex + 1 > waves.Length - 1)
        {
            Debug.Log("ALL WAVES COMPLETE! Looping...");
            nextWaveIndex = 0;
            nextWaveIndex = 1;
            // gameManager.LevelCompleted();
        }
        else
        {
            nextWaveIndex++;
        }
    }

    private bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (FindObjectsOfType<EnemyController>().Length == 0)
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning Wave: " + wave.name);
        state = SpawnState.SPAWNING;
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        state = SpawnState.WAITING;
    }

    private void SpawnEnemy(Transform enemy)
    {
        Debug.Log("Spawning Enemy: " + enemy.name);
        Transform transform = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, transform.position, transform.rotation);
    }
}
