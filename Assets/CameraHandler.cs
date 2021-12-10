using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    private Camera m_Camera;
    public float m_DampTime = 0.2f;
    private Vector3 m_MoveVelocity;
    public Transform m_Target;

    private void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = transform.position;
        desiredPosition.x = m_Target.position.x;

        // Smoothly transition to that position.
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref m_MoveVelocity, m_DampTime);
    }
}
