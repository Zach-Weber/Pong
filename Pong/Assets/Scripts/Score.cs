﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    //index 0 = right screen, index 1 = left screen
    private int[] playerScores = new int[2];
    public Text ScoreDisplay1, ScoreDisplay2;

    // Add the sound clip to this script
    public AudioClip ScoreSound;

    // Start is called before the first frame update
    void Start()
    {
        playerScores[0] = 0;
        playerScores[1] = 0;

        // Sets playonawake to false so the game doesnt accidentally play the sound
        GetComponent<AudioSource>().playOnAwake = false;
        // Sets the audio clip on the Audio Source component to the clip that was added to this script
        GetComponent<AudioSource>().clip = ScoreSound;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreDisplay1.text = "" + playerScores[0];
        ScoreDisplay2.text = "" + playerScores[1];
        CheckForWin();
    }

    public void IncrementScore(int player)
    {
        playerScores[player]++;

        // plays the score sound
        GetComponent<AudioSource>().Play();
    }

    public void CheckForWin()
    {
        // if player 1 reaches 13 points switch to player 1 win screen
        if (playerScores[0] == 11)
        {
            SceneManager.LoadScene("Player1Win");
        }
        // if player 2 reaches 13 points switch to player 2 win screen
        else if (playerScores[1] == 11)
        {
            SceneManager.LoadScene("Player2Win");
        }
    }
}
