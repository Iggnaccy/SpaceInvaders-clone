using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyTracker enemyTracker;
    [SerializeField] private EventController eventController;
    [SerializeField] private LevelSO level;
    [SerializeField] private Transform leftBoundary, rightBoundary;

    private int lastRowSpawned = 0;

    private void Start()
    {
        SpawnRow();
        eventController.OnWallContact.AddListener(SpawnRow);
    }

    private void SpawnRow()
    {
        if (lastRowSpawned < level.rows.Count)
        {
            var row = level.rows[lastRowSpawned];
            var count = row.enemies.Count;
            for (int i = 0; i < count; i++)
            {
                float t = count == 1 ? 0.5f : (float)i / (count - 1);
                var position = Vector3.Lerp(leftBoundary.position, rightBoundary.position, t);

                var enemy = row.enemies[i].poolManager.Get().GetComponent<Enemy>();
                enemy.transform.position = position;
                enemy.Setup(eventController);
                enemyTracker.AddEnemy(enemy);
            }
            lastRowSpawned++;
        }
    }
}
