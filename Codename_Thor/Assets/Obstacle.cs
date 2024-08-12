using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    public float spawnYPosition = -3.5f;

    void Start()
    {
        InvokeRepeating("SpawnObstacle", spawnInterval, spawnInterval);
    }

    void SpawnObstacle()
    {
        Vector2 spawnPosition = new Vector2(transform.position.x, spawnYPosition);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
