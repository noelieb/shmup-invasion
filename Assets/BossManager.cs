using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public Animator animator;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger("Attack_1");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            animator.SetTrigger("Walk_Cycle_1");
        }
    }
}
