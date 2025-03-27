using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private ScoreSO score;

    private void Start()
    {
        score.score = 0;
    }

    public void AddScore(EnemyStatsSO enemyStats, Enemy _)
    {
        score.score += enemyStats.scoreValue;
    }
}
