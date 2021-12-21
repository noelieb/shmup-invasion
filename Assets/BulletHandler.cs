using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{

    [SerializeField] GameObject m_ground_explosion;
    [SerializeField] GameObject m_object_explosion;
    [SerializeField] GameObject m_heart;
    [SerializeField] GameObject m_star;

    private float m_distanceCoeff = 0.3f;
    private float m_heartProba = 0.8f;
    private float m_BOOMProba = 0.9f;

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
        if (collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(m_ground_explosion, collision.contacts[0].point, Quaternion.identity);
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Enemy"))
        {
            float distance = Vector3.Distance(new Vector3(0,0, -30), collision.gameObject.transform.position);
            Instantiate(m_object_explosion, collision.contacts[0].point, Quaternion.identity);

            var random = Random.value;
            if(random > m_BOOMProba)
            {
                GameObject star = Instantiate(m_star, collision.contacts[0].point, Quaternion.Euler(-90,0,0));
                Vector3 position = star.transform.position;
                position.y = 0.0f;
                star.transform.SetPositionAndRotation(position, star.transform.rotation);
            }
            else if(random > m_heartProba)
            {
                GameObject heart = Instantiate(m_heart, collision.contacts[0].point, Quaternion.Euler(-90, 0, 0));
                Vector3 position = heart.transform.position;
                position.y = 0.0f;
                heart.transform.SetPositionAndRotation(position, heart.transform.rotation);
                //if (collision.gameObject.GetComponent<EnemyMovement>().IsMoving() == false)
                //{
                //    heart.GetComponent<EnemyMovement>().enabled = false;
                //}
            }
            userInterface.AddScore(10 + (int)(m_distanceCoeff*distance));
            Destroy(collision.gameObject);
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Boss"))
        {
            Instantiate(m_object_explosion, collision.contacts[0].point, Quaternion.identity);
            FindObjectOfType<BossManager>().DecreasePV(20);
            Destroy(gameObject);
        }
    }
}
