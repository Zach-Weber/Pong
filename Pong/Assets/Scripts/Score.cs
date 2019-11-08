﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    //index 0 = right screen, index 1 = left screen
    private int[] playerScores = new int[2];
    public Text ScoreDisplay1, ScoreDisplay2;

    // Start is called before the first frame update
    void Start()
    {
        playerScores[0] = 0;
        playerScores[1] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreDisplay1.text = "" + playerScores[0];
        ScoreDisplay2.text = "" + playerScores[1];
    }

    public void IncrementScore(int player)
    {
        playerScores[player]++;
    }

}