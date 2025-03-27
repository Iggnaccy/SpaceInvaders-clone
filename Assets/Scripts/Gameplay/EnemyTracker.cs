using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    [SerializeField] private EventController eventController;
    public List<Enemy> enemies;

    [ReadOnly] public float currentSpeed = 0;

    private void Start()
    {
        eventController.OnEnemyDeath.AddListener(OnEnemyDeath);
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        if (enemy.baseStats.minimumSpeed > currentSpeed)
        {
            currentSpeed = enemy.baseStats.minimumSpeed;
        }
    }

    private void OnEnemyDeath(EnemyStatsSO _, Enemy enemy)
    {
        enemies.Remove(enemy);
        if (enemy.baseStats.minimumSpeed == currentSpeed)
        {
            UpdateSpeed();
        }
    }

    private void UpdateSpeed()
    {
        currentSpeed = 0;
        foreach (var enemy in enemies)
        {
            if (enemy.baseStats.minimumSpeed > currentSpeed)
            {
                currentSpeed = enemy.baseStats.minimumSpeed;
            }
        }
    }
}


