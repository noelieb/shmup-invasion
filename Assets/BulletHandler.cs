using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{

    [SerializeField] GameObject m_ground_explosion;
    [SerializeField] GameObject m_object_explosion;
    private float m_coeff = 0.3f;

    private UserInterface userInterface;
    // Start is called before the first frame update
    void Start()
    {
        userInterface = FindObjectOfType<UserInterface>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(m_ground_explosion, collision.contacts[0].point, Quaternion.identity);
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Enemy"))
        {
            
            float distance = Vector3.Distance(new Vector3(0,0, -30), collision.gameObject.transform.position);
            Instantiate(m_object_explosion, collision.contacts[0].point, Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision.gameObject);
            userInterface.AugmentScore(10 + (int)(m_coeff*distance));
        }
    }
}
