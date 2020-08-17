using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemySpawnData[] spawnOptions;
    [SerializeField] private Transform playerTransform;

    [Header("Settings")]
    [SerializeField] private float playerDistanceThreshold = 4f;
    [SerializeField] private float maxSpawnTimer = 5f;
    [SerializeField] private float timerMultiplicator = 0.95f;
    [SerializeField] private float xRange;
    [SerializeField] private float yRange;

    private int totalWeight = 0;
    private float spawnTimer;

    private void Start()
    {
        foreach(var data in spawnOptions)
        {
            totalWeight += data.weight;
        }

        spawnTimer = Random.Range(0f, maxSpawnTimer);
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if(spawnTimer <= 0f)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var weightSelector = Random.Range(0, totalWeight);

        foreach(var data in spawnOptions)
        {
            if(weightSelector > data.weight)
            {
                weightSelector -= data.weight;
                continue;
            }

            Vector3 spawnPosition = Vector3.zero;

            while (spawnPosition == Vector3.zero)
            {
                Vector3 temp = new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange));

                if (Vector3.Distance(temp, playerTransform.position) > playerDistanceThreshold)
                {
                    spawnPosition = temp;
                }
            }

            var enemyObject = Instantiate(data.prefab, spawnPosition, Quaternion.identity);
            var enemy = enemyObject.GetComponent<EnemyMouse>();
            enemy.SetTarget(playerTransform);
            break;
        }

        maxSpawnTimer *= timerMultiplicator;
        spawnTimer = maxSpawnTimer;
    }
}
