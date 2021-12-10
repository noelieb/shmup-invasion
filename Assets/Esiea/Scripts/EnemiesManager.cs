using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_prefabs;
    [SerializeField] GameObject m_spawnPlane;
    [SerializeField] float delay = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Routine());
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    IEnumerator Routine()
    {
        while (true)
        {
            int aleat_dune = (int)(Random.value * m_prefabs.Length);
            
            List<Vector3> VerticeList = new List<Vector3>(m_spawnPlane.GetComponent<MeshFilter>().sharedMesh.vertices);
            Vector3 leftTop = m_spawnPlane.transform.TransformPoint(VerticeList[0]);
            Vector3 rightTop = m_spawnPlane.transform.TransformPoint(VerticeList[10]);
            Vector3 leftBottom = m_spawnPlane.transform.TransformPoint(VerticeList[110]);
            Vector3 rightBottom = m_spawnPlane.transform.TransformPoint(VerticeList[120]);
            Vector3 XAxis = rightTop - leftTop;
            Vector3 ZAxis = leftBottom - leftTop;
            Vector3 RndPointonPlane = leftTop + XAxis * Random.value + ZAxis * Random.value;


            Quaternion randrotation = Quaternion.Euler(0, 0, 0);
            GameObject enemyInstance = Instantiate(m_prefabs[aleat_dune], RndPointonPlane, randrotation);
            //enemyInstance.transform.localScale = new Vector3(getRandom(0.6f, 0.8f), getRandom(0.6f, 0.8f), getRandom(0.6f, 0.8f));
            enemyInstance.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            yield return new WaitForSeconds(delay);
        }
    }
    float getRandom(float min, float max)
    {
        return Random.value * (max - min) + min;
    }



    
}
