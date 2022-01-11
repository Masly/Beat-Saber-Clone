using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ScoreService : MonoBehaviour, IScoreService
{
    private int score = 0;
    [SerializeField]
    private ScoreDisplayer scoreDisplayer;
    int IScoreService.score => score;
    private void OnValidate()
    {
        Assert.IsNotNull(scoreDisplayer);
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreDisplayer.UpdateScoreUI(score);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreDisplayer.UpdateScoreUI(score);
    }
}
