using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] SpawnObject[] m_spawnObjects;
    [SerializeField] float m_delay;

    private bool m_spawning;
    private float m_totalFrequencies;
    private IEnumerator m_spawningRoutine;


    public bool IsSpawning() { return m_spawning; }

    

    private void Start()
    {
        m_spawning = true;
        m_totalFrequencies = 0.0f;
        for (int i = 0; i < m_spawnObjects.Length; i++)
        {
            m_totalFrequencies += m_spawnObjects[i].m_frequency;
            GameObject plane = m_spawnObjects[i].m_spawnPlane;
            List<Vector3> VerticeList = new List<Vector3>(plane.GetComponent<MeshFilter>().sharedMesh.vertices);
            m_spawnObjects[i].leftTop = plane.transform.TransformPoint(VerticeList[0]);
            m_spawnObjects[i].rightTop = plane.transform.TransformPoint(VerticeList[10]);
            m_spawnObjects[i].leftBottom = plane.transform.TransformPoint(VerticeList[110]);
            m_spawnObjects[i].rightBottom = plane.transform.TransformPoint(VerticeList[120]);
            m_spawnObjects[i].XAxis = m_spawnObjects[i].rightTop - m_spawnObjects[i].leftTop;
            m_spawnObjects[i].ZAxis = m_spawnObjects[i].leftBottom - m_spawnObjects[i].leftTop;
        }
        m_spawningRoutine = Spawn();
        StartCoroutine(m_spawningRoutine);
    }

    IEnumerator Spawn()
    {
        while (Application.isPlaying)
        {
            SpawnObject spawnObject = GetRandomSpawnObject();
            Vector3 randomPosition = spawnObject.leftTop + spawnObject.XAxis * UnityEngine.Random.value + spawnObject.ZAxis * UnityEngine.Random.value;


            GameObject spawnInstance = Instantiate(spawnObject.m_prefab, randomPosition, Quaternion.identity);
           
            GameObject child = spawnInstance.transform.GetChild(0).gameObject;
            child.transform.Rotate(0, GetRandomFloat(0, 360), 0);
            float scale = GetRandomFloat(spawnObject.m_scaleMin, spawnObject.m_scaleMax);
            child.transform.localScale = scale * Vector3.one;

            yield return new WaitForSeconds(m_delay);
        }
    }

    public void StopSpawning()
    {
        StopCoroutine(m_spawningRoutine);
        m_spawning = false;
    }

    float GetRandomFloat(float min, float max)
    {
        return UnityEngine.Random.value * (max - min) + min;
    }

    SpawnObject GetRandomSpawnObject()
    {
        float rand = UnityEngine.Random.value * m_totalFrequencies;
        float currentProb = 0;

        foreach (var spawnObject in m_spawnObjects)
        {
            currentProb += spawnObject.m_frequency;
            if (rand <= currentProb)
                return spawnObject;
        }
        return new SpawnObject();
    }


}

[System.Serializable]
public struct SpawnObject
{
    [SerializeField] public GameObject m_prefab;
    [SerializeField] public GameObject m_spawnPlane;
    [SerializeField] public float m_frequency;
    [SerializeField] public float m_scaleMin;
    [SerializeField] public float m_scaleMax;

    [HideInInspector] public Vector3 leftTop;
    [HideInInspector] public Vector3 rightTop;
    [HideInInspector] public Vector3 leftBottom;
    [HideInInspector] public Vector3 rightBottom;
    [HideInInspector] public Vector3 XAxis;
    [HideInInspector] public Vector3 ZAxis;
}


