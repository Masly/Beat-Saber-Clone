using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public int score = 0;
    public ScoreDisplayer scoreDisplayer;

    private void Start()
    {
        //Set up singleton
        if (singleton && singleton != this)
            Destroy(this);
        else
            singleton = this;
        scoreDisplayer.UpdateScoreUI(score);
    }


    public void AddScore(int amount)
    {
        score += amount;
        scoreDisplayer.UpdateScoreUI(score);
    }



}
