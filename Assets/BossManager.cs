using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class BossManager : MonoBehaviour
{
    public Animator animator;
    private GameObject player;
    private Vector3 m_MoveVelocity;
    private int m_pv = 100;
     
    struct Delay
    {
        public Stopwatch s_stopwatch;
        public float s_timeBetween;

        public Delay(Stopwatch stopwatch, float time)
        {
            s_timeBetween = time;
            s_stopwatch = stopwatch;
        }
    }

    private Dictionary<string, Delay> actions = new Dictionary<string, Delay>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        actions.Add("attack", new Delay(new Stopwatch(), 2f));
        actions.Add("target", new Delay(new Stopwatch(), 1f));
        actions.Add("walk", new Delay(new Stopwatch(), 5f));

        UnityEngine.Debug.Log(actions);
        foreach (var action in actions)
        {
            action.Value.s_stopwatch.Start();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);


        if (dist < 30 && actions["attack"].s_stopwatch.Elapsed.TotalSeconds > actions["attack"].s_timeBetween)
        {
            UnityEngine.Debug.Log("attaque");
            actions["attack"].s_stopwatch.Restart();
            int random = (int)(UnityEngine.Random.value * 5 + 1);
            UnityEngine.Debug.Log(random);
            animator.SetTrigger("Attack_" + random);
            FindObjectOfType<UserInterface>().AddPV(-20 - (int)(0.3 * dist));
        }

        if (dist < 50 && actions["target"].s_stopwatch.Elapsed.TotalSeconds > actions["target"].s_timeBetween)
        {
            UnityEngine.Debug.Log("target");
            actions["target"].s_stopwatch.Restart();
            //animator.SetTrigger("Walk_Cycle_1");
            Vector3 direction = player.transform.position - transform.position;
            Vector3 current = transform.rotation.eulerAngles;
            Vector3 target = Quaternion.LookRotation(direction).eulerAngles;
            transform.rotation = Quaternion.Euler(Vector3.SmoothDamp(current, target, ref m_MoveVelocity, 0.3f));
        }

        if (actions["walk"].s_stopwatch.Elapsed.TotalSeconds > actions["walk"].s_timeBetween && UnityEngine.Random.value > 0.95f)
        {
            //UnityEngine.Debug.Log("viens");
            //animator.SetTrigger("Walk_Cycle_1");

            //actions["walk"].s_stopwatch.Restart();

            //transform.position = Vector3.SmoothDamp(transform.position, transform.position + 1 * Vector3.Normalize(player.transform.position - transform.position), ref m_MoveVelocity, 0.3f);
        }
    }

    internal void DecreasePV(int value)
    {
        m_pv = m_pv - value;
        FindObjectOfType<UserInterface>().SetPVBoss(m_pv);
        if (m_pv <= 0)
        {
            FindObjectOfType<gameManager>().Win();
        }
    }
}
