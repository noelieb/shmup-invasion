using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class gameManager : MonoBehaviour
{
    [SerializeField] Spawner terrainSpawner;
    [SerializeField] Spawner enemiesSpawner;

    private float m_timeBeforeBoss = 10f;

    private Stopwatch stopwatch;

    private void Awake()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (terrainSpawner.IsSpawning())
        {
            if(stopwatch.Elapsed.TotalSeconds > m_timeBeforeBoss)
            {
                PrepareBossFight();
            }        
        }
    }

    private void PrepareBossFight()
    {
        terrainSpawner.StopSpawning();
        enemiesSpawner.StopSpawning();
    }
}
