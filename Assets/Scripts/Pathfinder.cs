using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to the 'Enemy 0' prefab

// This script manages:
// - Linking together WaveConfigSO and EnemySpawner to follow the waypoint path from WaveConfigSO
// - Moves enemy every frame closer to the next waypoint

public class Pathfinder : MonoBehaviour
{
    // these two variables are used to reference and get the WaveConfigSO from EnemySpawner script, ...
    // so we don't have to SerializeField the WaveConfigSO for Pathfinder, in case we forget or ...
    // use another SO by accident
    WaveConfigSO waveConfig;
    EnemySpawner enemySpawner;

    List<Transform> waypoints; // store waypoints for our path, populated from WaveConfigSO
    int waypointIndex = 0; // tracks the waypoint that the enemy is currently on

    void Awake() 
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave(); // gets the current wave (WaveConfigSO) from EnemySpawner script
        waypoints = waveConfig.GetWaypoints(); // populate list of waypoints
        transform.position = waypoints[waypointIndex].position; // move enemy to first waypoint
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath() // every frame, move enemy close to next waypoint
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime; // distance moving each frame
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta); // moves enemy
            if (transform.position == targetPosition) // incremement waypoint index
            {
                waypointIndex++;
            } 
        }
        else Destroy(gameObject);
    }
}
