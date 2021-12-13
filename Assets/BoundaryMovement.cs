using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryMovement : MonoBehaviour
{
    [SerializeField] float m_Speed;
    private bool m_isMoving;
    // Start is called before the first frame update
    void Start()
    {
        m_isMoving = true;
        if (m_Speed == 0)
        {
            m_Speed = 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isMoving)
        {
            if (transform.position.z < -85)
            {
                transform.Translate(transform.forward*-360);
            }
            transform.Translate(transform.forward * m_Speed * Time.deltaTime);
        }
    }

    internal void StopMoving()
    {
        m_isMoving = false;
    }
}
