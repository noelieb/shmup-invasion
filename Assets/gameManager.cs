using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    [SerializeField] Spawner terrainSpawner;
    [SerializeField] Spawner enemiesSpawner;
    [SerializeField] GameObject fond;

    [SerializeField] GameObject m_gameover;
    [SerializeField] GameObject m_win;
    [SerializeField] Text m_scoretext;
    private EnemyMovement fondMovement;

    [SerializeField] float m_timeBeforeBoss = 10f;
    [SerializeField] GameObject m_monster;

    private Stopwatch stopwatch;
    private CameraHandler cameraHandler;

    private bool ending = false;

    private void Awake()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
        fondMovement = fond.GetComponent<EnemyMovement>();
        fondMovement.enabled = false;
        ending = false;
        cameraHandler = FindObjectOfType(typeof(CameraHandler)) as CameraHandler;
        m_gameover.SetActive(false);
        m_win.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameOver()
    {
        ending = true;
        m_gameover.SetActive(true);
    }

    internal void Win()
    {
        ending = true;
        m_win.SetActive(true);
        var score = FindObjectOfType<UserInterface>().getScore();
        m_scoretext.text = "Your score was : "+score;
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
        if (ending)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    private void PrepareBossFight()
    {
        SpawnBoss();
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

    void SpawnBoss()
    {
        Instantiate(m_monster, new Vector3(0,0,270), Quaternion.Euler(180*Vector3.up));
    }


}
