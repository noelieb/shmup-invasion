using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float m_Speed;

    [SerializeField] bool overridespecial = false;

    private bool m_isMoving;

    public bool IsMoving() { return m_isMoving; }


    // Start is called before the first frame update
    private void Awake()
    {
        if (m_Speed == 0.0)
        {
            m_Speed = 10.0f;
        }
        m_isMoving = true;
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_isMoving)
        {
            Move();
        }
    }

    private void Move()
    {
        if (overridespecial)
        {
            Vector3 movement = new Vector3(0,0,-1) * m_Speed * Time.deltaTime;
            transform.SetPositionAndRotation(transform.position + movement, transform.rotation);
        }
        else
        {
            Vector3 movement = -transform.forward * m_Speed * Time.deltaTime;
            transform.SetPositionAndRotation( transform.position + movement, transform.rotation);
        }
    }

    public void StopMoving()
    {
        m_isMoving = false;
    }

}
