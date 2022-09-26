using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] WaveConfigSO waveConfig;
    List<Transform> waypoints; // store waypoints for our path, populated from WaveConfigSO
    int waypointIndex = 0; // tracks the waypoint that the enemy is currently on

    void Start()
    {
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
