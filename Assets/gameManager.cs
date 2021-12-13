using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class gameManager : MonoBehaviour
{
    [SerializeField] Spawner terrainSpawner;
    [SerializeField] Spawner enemiesSpawner;
    [SerializeField] GameObject fond;
    private EnemyMovement fondMovement;

    [SerializeField] float m_timeBeforeBoss = 10f;
    [SerializeField] GameObject m_monster;

    private Stopwatch stopwatch;
    private CameraHandler cameraHandler;
    

    private void Awake()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
        fondMovement = fond.GetComponent<EnemyMovement>();
        fondMovement.enabled = false;
        cameraHandler = FindObjectOfType(typeof(CameraHandler)) as CameraHandler;
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
        UnityEngine.Debug.Log("bossfight");
        SpawnEnemy();
        stopwatch.Restart();
        terrainSpawner.StopSpawning();
        enemiesSpawner.StopSpawning();
        fondMovement.enabled = true;
        StartCoroutine(GetToEdge());
        cameraHandler.BossFight();
    }


    IEnumerator GetToEdge()
    {
        yield return new WaitForSeconds(20);

        fondMovement.enabled = false;

        EnemyMovement[] enemyMovements = FindObjectsOfType(typeof(EnemyMovement)) as EnemyMovement[];
        foreach (EnemyMovement enemy in enemyMovements)
        {
            enemy.StopMoving();
        }
        BoundaryMovement[] boundaryMovements = FindObjectsOfType(typeof(BoundaryMovement)) as BoundaryMovement[];
        foreach (BoundaryMovement boundary in boundaryMovements)
        {
            boundary.StopMoving();
        }
    }

    void SpawnEnemy()
    {
        Instantiate(m_monster, new Vector3(0,0,270), Quaternion.identity);
    }
}
