using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private float levelLength;
    [SerializeField] private float waitPeriodLength;
    [Header("VFX")]
    [Tooltip("The main playable area that will change colors when the level changes.")]
    [SerializeField] private GameObject arena;
    [Tooltip("Material to change the level too that will signify the rainbow level phase.")]
    [SerializeField] private Material rainbowScrollMaterial;

    // Level Progression
    private float timer;
    private bool isLevel = true;
    // VFX
    private List<Material> arenaMaterials = new List<Material>();
    private string shaderColorFieldUID = "Color_7A7AB713";
    List<TilemapRenderer> tmrComponents;
    
    internal virtual void Awake()
    {
        // Get and store the starting materials for the wall and floor. Well need them after we transition
        // back from the rainbow level
        tmrComponents = new List<TilemapRenderer>(arena.GetComponentsInChildren<TilemapRenderer>());
        foreach (TilemapRenderer tmr in tmrComponents)
        {
            arenaMaterials.Add(tmr.sharedMaterial);
        }
    }

    internal virtual void Update()
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
                // Change the color of the level
                SetLevelColorOverlay(Levels.Colors[type]);
            }
        }
    }

    // Sets a color addative for the wall and floor. If Color.black is provided, the rainbow level will begin.
    private void SetLevelColorOverlay(Color color)
    {
        if (color == Color.black)
        {
            // Rainbow level
            for(int i = 0; i < tmrComponents.Count; i++)
            {
                tmrComponents[i].material = rainbowScrollMaterial;
            }
        }
        else
        {
            // Standard color levels
            for(int i = 0; i < tmrComponents.Count; i++)
            {
                tmrComponents[i].material = arenaMaterials[i];
                tmrComponents[i].material.SetColor(shaderColorFieldUID, color);
            }
        }
    }
}
