using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreService
{
    public int score
    {
        get;
    }
    public void AddScore(int amount);
}
