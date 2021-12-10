using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rikayon : MonoBehaviour {

    public Animator animator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

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
