using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    //index 0 = right screen, index 1 = left screen
    private int[] playerScores = { 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncrementScore(int player)
    {
        playerScores[player]++;
    }

}
