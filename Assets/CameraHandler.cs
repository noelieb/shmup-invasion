using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    private Camera m_Camera;
    public float m_DampTime = 0.2f;
    private Vector3 m_MoveVelocity;
    public Transform m_Target;

    private bool m_bossfight;

    private void Awake()
    {
        m_bossfight = false;
        m_Camera = GetComponentInChildren<Camera>();
    }

    public void BossFight()
    {
        m_bossfight = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = Vector3.zero;
        if (m_bossfight)
        {
            desiredPosition = transform.position;
            desiredPosition.x = m_Target.position.x;
            desiredPosition.z = m_Target.position.z - 10;
        }
        else
        {
            desiredPosition = transform.position;
            desiredPosition.x = m_Target.position.x;
        }

        // Smoothly transition to that position.
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref m_MoveVelocity, m_DampTime);
    }
}
