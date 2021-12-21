using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody m_Rigidbody;                      // rigidbody du tank
    [SerializeField] float m_Speed;

    private UserInterface userInterface;

    private Camera m_camera;



    // Start is called before the first frame update
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        if(m_Speed == 0.0)
        {
            m_Speed = 10.0f;
        }
        userInterface = FindObjectOfType<UserInterface>();
        m_camera = FindObjectOfType<Camera>();
    }

    void FixedUpdate()
    {
        Move();
        CheckOutsideScreen();
    }


    private void CheckOutsideScreen()
    {
        if(m_camera.WorldToScreenPoint(transform.position).y < -50)
        {
            FindObjectOfType<gameManager>().GameOver();
        }
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
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.z > -40)
        {
            Vector3 movement = -transform.forward * m_Speed * Time.deltaTime;
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                userInterface.AddPV(-15);
                break;
            case "Heart":
                userInterface.AddPV(+15);
                Destroy(collision.gameObject);
                break;
            case "Star":
                FindObjectOfType<PlayerShooting>().Boom();
                Destroy(collision.gameObject);
                break;
            default:
                break;
        }
    }
}
