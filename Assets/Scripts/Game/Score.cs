﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI m_scoreText = null;

    public int score { get; set; }
    public int targetScore { get; set; }

    void Awake()
    {
        score = 0;
    }
	
	public void AddPoints(int points)
    {
        score = score + points;
        m_scoreText.text = score.ToString();
	}
}
