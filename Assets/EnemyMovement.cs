using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float m_Speed;


    // Start is called before the first frame update
    private void Awake()
    {
        if (m_Speed == 0.0)
        {
            m_Speed = 10.0f;
        }
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = -transform.forward * m_Speed * Time.deltaTime;
        transform.SetPositionAndRotation( transform.position + movement, transform.rotation);
    }
}
