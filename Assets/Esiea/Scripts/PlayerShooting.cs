using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Transform m_bulletSpawnerTransform;
    [SerializeField] Rigidbody m_bullet;
    [SerializeField] float m_msBetweenShots;

    private Stopwatch stopwatch;
    private UserInterface userInterface;


    private void Awake()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();

        userInterface = FindObjectOfType<UserInterface>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(m_msBetweenShots == 0)
        {
            m_msBetweenShots = 500;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && stopwatch.Elapsed.TotalMilliseconds > m_msBetweenShots)
        {
            Rigidbody shellInstance =
                Instantiate(m_bullet, m_bulletSpawnerTransform.position, m_bulletSpawnerTransform.rotation) as Rigidbody;
            shellInstance.velocity = 40 * m_bulletSpawnerTransform.forward;
            stopwatch.Restart();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        userInterface.DecreasePV(15);
    }
}
