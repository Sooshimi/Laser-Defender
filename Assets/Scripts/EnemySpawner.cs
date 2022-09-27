using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to the 'EnemySpawner' game object

// This script manages:
// - Spawning enemies with varied spawn times for each wave
// - Spawning multiple waves

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs; // list of wave configs to spawn multiple waves
    [SerializeField] float timeBetweenWaves = 0f; // spawn time between waves
    WaveConfigSO currentWave; // stores enemy list for each wave config in waveConfig list

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave() // lets other scripts reference the same waveConfigSO this is looping through
    {
        return currentWave;
    }

    // loops through all enemies from WaveConfigSO and spawns them with varied spawn times
    IEnumerator SpawnEnemyWaves() 
    {
        // using Foreach, because we need to loop through all waves and don't need to keep track of an iterator
        foreach(WaveConfigSO wave in waveConfigs)
        {
            currentWave = wave; // sets currentWave to each specific waveConfigSO it's looping through

            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                Instantiate // create object in runtime
                (
                    currentWave.GetEnemyPrefab(i), // get enemy at index
                    currentWave.GetStartingWaypoint().position, // get its position
                    Quaternion.identity, // no rotation
                    transform // transform of the parent (the Enemy Spawner itself)
                ); 
                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime()); // Coroutine for our customised varied spawn times
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}
