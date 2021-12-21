using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Transform m_bulletSpawnerTransform;
    [SerializeField] Rigidbody m_bullet;
    [SerializeField] float m_msBetweenShots;

    [SerializeField] Slider m_fireSlider;

    private float m_MinFireForce = 15f;
    private float m_MaxFireForce = 30f;        
    private float m_MaxChargingTime = 0.75f;
    private float m_CurrentFireForce;
    private float m_ChargingSpeed;
    private bool m_Fired;

    public AudioSource m_ShootingAudio;        
    public AudioSource m_ShootingAudioBoom;        
    public AudioClip m_ChargingAudio;       
    public AudioClip m_FireAudio;
    public AudioClip m_BoomAudio;

    private Stopwatch stopwatch;

    private float[] m_boomtimes = { 0.087f, 0.515f - 0.087f, 0.952f - 0.515f, 1.382f - 0.952f};


    private void Awake()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
        m_CurrentFireForce = m_MinFireForce;
        m_ChargingSpeed = (m_MaxFireForce - m_MinFireForce) / m_MaxChargingTime;
        m_Fired = false;
        m_fireSlider.value = m_MinFireForce;

    }
    // Start is called before the first frame update
    void Start()
    {
        if(m_msBetweenShots == 0)
        {
            m_msBetweenShots = 500;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_fireSlider.value = m_MinFireForce;
        if (m_CurrentFireForce >= m_MaxFireForce && !m_Fired)
        {
            m_CurrentFireForce = m_MaxFireForce;
            Fire();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Fired = false;
            m_CurrentFireForce = m_MinFireForce;

            m_ShootingAudio.clip = m_ChargingAudio;
            m_ShootingAudio.Play();
        }
        else if (Input.GetKey(KeyCode.Space) && !m_Fired)
        {
            m_CurrentFireForce += m_ChargingSpeed * Time.deltaTime;

            m_fireSlider.value = m_CurrentFireForce;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && !m_Fired)
        {
            Fire();
        }
    }

    private void Fire()
    {
        m_Fired = true;
        Rigidbody shellInstance =
               Instantiate(m_bullet, m_bulletSpawnerTransform.position, m_bulletSpawnerTransform.rotation) as Rigidbody;
        shellInstance.velocity = m_CurrentFireForce * m_bulletSpawnerTransform.forward;
        stopwatch.Restart();


        m_ShootingAudio.clip = m_FireAudio;
        m_ShootingAudio.Play();

        m_CurrentFireForce = m_MinFireForce;
    }

    internal void Boom()
    {
        m_ShootingAudioBoom.clip = m_BoomAudio;
        m_ShootingAudioBoom.Play();
        StartCoroutine(BoomRoutine());
    }

    IEnumerator BoomRoutine()
    {
        for (int i = 0; i < 4; i++){
            yield return new WaitForSeconds(m_boomtimes[i]);
            Fire();
        }
    }
}
