using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private float levelLength;
    [SerializeField] private float waitPeriodLength;

    private float timer;
    private bool isLevel = true;

    void Update()
    {
        timer += Time.deltaTime;

        if (isLevel)
        {
            if (timer > levelLength)
            {
                timer -= levelLength;
                spawner.isWorking = false;
                isLevel = false;
            }
        }
        else
        {
            if (timer > waitPeriodLength)
            {
                timer -= waitPeriodLength;
                spawner.isWorking = true;
                isLevel = true;

                var values = LevelType.GetValues(typeof(LevelType));
                LevelType type = (LevelType)values.GetValue(Random.Range(0, values.Length));
                spawner.GenerateWeights(type);
            }
        }
    }
}
