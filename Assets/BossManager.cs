using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class BossManager : MonoBehaviour
{
    public Animator animator;
    private GameObject player;
    private Stopwatch m_stopwatchAttack;
    private Stopwatch m_stopwatchWalk;

    private float m_timeBetweenAttacks = 2f;
    private float m_timeBetweenTargetSet = 1f;

    private Vector3 m_MoveVelocity;

    struct Delay
    {
        Stopwatch s_stopwatch;
        float s_timeBetween;

        public Delay(Stopwatch stopwatch, float time)
        {
            s_timeBetween = time;
            s_stopwatch = stopwatch;
        }
    }

    private Dictionary<string, Delay> actions;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        actions.Add("attack", new Delay(new Stopwatch(), 2f));


        m_stopwatchAttack = new Stopwatch();
        m_stopwatchWalk = new Stopwatch();
        m_stopwatchAttack.Start();
        m_stopwatchWalk.Start();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);


        if (dist < 30 && m_stopwatchAttack.Elapsed.TotalSeconds > m_timeBetweenAttacks)
        {
            int random = (int)(Random.value * 5 + 1);
            UnityEngine.Debug.Log(random);
            animator.SetTrigger("Attack_" + random);
            m_stopwatchAttack.Restart();
        }

        if (dist < 50 && m_stopwatchAttack.Elapsed.TotalSeconds > m_timeBetweenTargetSet)
        {
            m_stopwatchWalk.Restart();
            animator.SetTrigger("Walk_Cycle_1");
            Vector3 direction = player.transform.position - transform.position;
            Vector3 current = transform.rotation.eulerAngles;
            Vector3 target = Quaternion.LookRotation(direction).eulerAngles;
            transform.rotation = Quaternion.Euler(Vector3.SmoothDamp(current, target, ref m_MoveVelocity, 0.3f));
        }
        if (m_stopwatchAttack.Elapsed.TotalSeconds > m_timeBetweenTargetSet && Random.value > 0.5)
        {
            if ()
            {
                UnityEngine.Debug.Log("viens");
                transform.position = Vector3.SmoothDamp(transform.position, transform.position + 1 * (player.transform.position - transform.position), ref m_MoveVelocity, 0.3f);
            }
        }
    }
    }
}
