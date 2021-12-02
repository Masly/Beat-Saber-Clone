using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * JUST FOR TESTING! 
 */

public class ScoreDisplayer : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public string label = "Score: ";
    public void UpdateScoreUI(int score)
    {
        scoreText.SetText(label + score);
    }
}
