using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private float levelLength;
    [SerializeField] private float waitPeriodLength;
    [SerializeField] private GameObject arena;
    [SerializeField] private Material rainbowScrollMaterial;

    // Level Progression
    private float timer;
    private bool isLevel = true;
    // VFX
    private List<Material> arenaMaterials = new List<Material>();
    private string shaderColorFieldUID = "Color_7A7AB713";
    TilemapRenderer[] tmrComponents;
    
    private void Awake()
    {
        // Get and store the materials for the wall and floor
        tmrComponents = arena.GetComponentsInChildren<TilemapRenderer>();
        foreach (TilemapRenderer tmr in tmrComponents)
        {
            arenaMaterials.Add(tmr.sharedMaterial);
        }
    }

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
                SetLevelColorOverlay(Color.gray);
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
                SetLevelColorOverlay(Levels.Colors[type]);
            }
        }
    }

    // Sets a color addative for the wall and floor.
    private void SetLevelColorOverlay(Color color)
    {
        if (color == Color.black)
        {
            for(int i = 0; i < tmrComponents.Length; i++)
            {
                tmrComponents[i].material = rainbowScrollMaterial;
            }
        }
        else
        {
            for(int i = 0; i < tmrComponents.Length; i++)
            {
                tmrComponents[i].material = arenaMaterials[i];
                tmrComponents[i].material.SetColor(shaderColorFieldUID, color);
            }
        }
    }
}
