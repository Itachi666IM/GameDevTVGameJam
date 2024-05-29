using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState {SPAWNING, COUNTING, WAITING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float spawnRate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if(state == SpawnState.WAITING)
        {
            //Check if enemies are still alive
            if(!IsEnemyAlive())
            {
                //Begin a new wave
                WaveCompleted();

            }
            else
            {
                return;
            }
        }
        if(waveCountdown<=0)
        {
            if(state!=SpawnState.SPAWNING)
            {
                //Start Spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave " + _wave.name);
        state = SpawnState.SPAWNING;
        for(int i=0;i<_wave.count;i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.spawnRate);
        }
        //Spawn

        state = SpawnState.WAITING;
        //Waiting for player to kill the enemies
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //Spawn Enemy
        Debug.Log("Spawning Enemy");
        Instantiate(_enemy, transform.position, transform.rotation);
    }

    bool IsEnemyAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown<=0)
        {
            searchCountdown = 1f;
        if(GameObject.FindGameObjectWithTag("Enemy")==null)
        {
            return false;
        }
        }
        return true;
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave+1>waves.Length-1)
        {
            Debug.Log("DEFEATED ALL ENEMIES");
            nextWave = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            nextWave++;
        }
    }
}
