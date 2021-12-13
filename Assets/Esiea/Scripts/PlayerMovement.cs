using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody m_Rigidbody;                      // rigidbody du tank
    [SerializeField] float m_Speed;
    private ParticleSystem[] m_particleSystems;


    // Start is called before the first frame update
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        if(m_Speed == 0.0)
        {
            m_Speed = 10.0f;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
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
}
