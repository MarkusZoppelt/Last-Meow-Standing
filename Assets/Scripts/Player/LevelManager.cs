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
    [Tooltip("The walls of the main playable area that will change colors when the level changes.")]
    [SerializeField] private GameObject arenaWalls;
    [Tooltip("Material to change the walls to that will signify the rainbow level phase.")]
    [SerializeField] private Material rainbowScrollMaterial;

    // Level Progression
    private float timer = 0;
    private bool isLevel = true;
    // VFX
    private List<Material> wallMaterials = new List<Material>();
    private string shaderColorFieldUID = "Color_7A7AB713"; // Keep this in sync with the "Color_Additive" field in "TileMapColorChange.shadergraph"
    private List<TilemapRenderer> tmrComponents;
    
    internal virtual void Awake()
    {
        // Get and store the starting materials for the wall. Well need them after we transition
        // back from the rainbow level.
        tmrComponents = new List<TilemapRenderer>(arenaWalls.GetComponents<TilemapRenderer>());
        foreach (TilemapRenderer tmr in tmrComponents)
        {
            wallMaterials.Add(tmr.sharedMaterial);
        }
        SetLevelColorOverlay(Levels.Colors[LevelType.White]);
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
                isLevel = true;
                spawner.isWorking = true;
            }
            else
            {
                var values = LevelType.GetValues(typeof(LevelType));
                LevelType type = (LevelType)values.GetValue(Random.Range(0, values.Length));
                spawner.GenerateWeights(type);
                SetLevelColorOverlay(Levels.Colors[type]);
            }
        }
    }

    // Sets a color addative for the walls. If Color.black is provided, the rainbow level will begin.
    private void SetLevelColorOverlay(Color color)
    {
        if (color == Color.black)
        {
            // Rainbow level.
            for(int i = 0; i < tmrComponents.Count; i++)
            {
                tmrComponents[i].material = rainbowScrollMaterial;
            }
        }
        else
        {
            // Standard color levels.
            for(int i = 0; i < tmrComponents.Count; i++)
            {
                tmrComponents[i].material = wallMaterials[i];
                tmrComponents[i].material.SetColor(shaderColorFieldUID, color);
            }
        }
    }
}
