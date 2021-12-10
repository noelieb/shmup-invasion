using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody m_Rigidbody;                      // rigidbody du tank
    private ParticleSystem[] m_particleSystems;
    [SerializeField] Transform m_bulletShooterTransform;
    [SerializeField] Rigidbody m_bullet;                // bullet à instancier
    [SerializeField] float m_Speed;


    // Start is called before the first frame update
    void Start()
    {
        //m_particleSystems = GetComponentsInChildren<ParticleSystem>();
        //for (int i = 0; i < m_particleSystems.Length; ++i)
        //{
        //    m_particleSystems[i].Play();
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerShooting();
        PlayerMovement();
    }

    private void PlayerShooting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Rigidbody shellInstance =
                Instantiate(m_bullet, m_bulletShooterTransform.position, m_bulletShooterTransform.rotation) as Rigidbody;
            shellInstance.velocity = 1 * transform.forward;
        }
    }

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 movement = -transform.right * m_Speed * Time.deltaTime;
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 movement = transform.right * m_Speed * Time.deltaTime;
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 movement = transform.forward * m_Speed * Time.deltaTime;
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 movement = -transform.forward * m_Speed * Time.deltaTime;
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }

    }

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

}
