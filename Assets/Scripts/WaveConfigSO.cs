using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This SO manages:
// - Getting all waypoint child objects from the 'Path #' prefabs
// - Setting the move speed of enemy pathing
// - Instantiate enemies and set their varied spawn times in the wave

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs; // contains all enemies
    [SerializeField] Transform pathPrefab; // contains all waypoints
    [SerializeField] float moveSpeed = 5f; // path move speed
    [SerializeField] float timeBetweenEnemySpawns = 1f; // specify spawn time of each enemy in the wave
    [SerializeField] float spawnTimeVariance = 0.5f; // add variance to enemy spawn times. 0 = no variance, >0 = variance
    [SerializeField] float minimumSpawnTime = 0.2f;

    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>(); // empty list of waypoints to be stored
        foreach(Transform child in pathPrefab) // add waypoints for each child of pathPrefab parent
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    public float GetRandomSpawnTime()
    {
        // store random range
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, // lower bound
                                        timeBetweenEnemySpawns + spawnTimeVariance);  // upper bound

        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue); // sets min and max range of spawnTime
    }
}
