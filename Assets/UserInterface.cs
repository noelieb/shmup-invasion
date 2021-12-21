using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    private int m_score;
    private int m_playerPV;
    private Text m_scoreText;
    [SerializeField] Slider m_pvSlider;
    [SerializeField] Slider m_BossPVSlider;

    // Start is called before the first frame update
    void Start()
    {
        m_score = 0;
        m_playerPV = 0;

        m_scoreText = GetComponentInChildren<Text>();
        
        UpdateScore();
        UpdatePV();
    }

    private void UpdatePV()
    {
        m_pvSlider.value = m_playerPV;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int value)
    {
        m_score += value;
        UpdateScore();
    }

    public void AddPV(int value)
    {
        m_playerPV -= value;
        if(m_playerPV < 0) { m_playerPV = 0; }
        if (m_playerPV >= 100)
        {
            FindObjectOfType<gameManager>().GameOver();
        }
        UpdatePV();
    }



    private void UpdateScore()
    {
        m_scoreText.text = "Score : "+m_score.ToString();
    }

    internal void SetPVBoss(int m_pv)
    {
        m_BossPVSlider.value = m_pv;
    }

    public int getScore()
    {
        return m_score;
    }
}
