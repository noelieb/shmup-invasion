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
    private Slider m_pvSlider;

    // Start is called before the first frame update
    void Start()
    {
        m_score = 0;
        m_playerPV = 100;

        m_scoreText = GetComponentInChildren<Text>();
        m_pvSlider = GetComponentInChildren<Slider>();
        
        UpdateScore();
        UpdatePV();
    }

    private void UpdatePV()
    {
        m_pvSlider.value = m_playerPV / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AugmentScore(int value)
    {
        m_score += value;
        UpdateScore();
    }

    public void DecreasePV(int value)
    {
        m_playerPV -= value;
        UpdatePV();
    }



    private void UpdateScore()
    {
        m_scoreText.text = "Score : "+m_score.ToString();
    }
}
