using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyTracker enemyTracker;
    [SerializeField] private EventController eventController;
    [SerializeField] private SimpleSettingsSO settings;
    [SerializeField] private Transform leftBoundary, rightBoundary; // need to move spawn boundaries alongside enemies to prevent them from desyncing
    private float direction = 1f; // 1 is right, -1 is left
    [ReadOnly, SerializeField] private float remainingDownwardMovement = 0;

    private void Start()
    {
        eventController.OnWallContact.AddListener(ReverseDirection);
        remainingDownwardMovement = settings.rowHeight * settings.movementUnitConversionRate;
    }

    private void Update()
    {
        Vector3 distance;
        if (remainingDownwardMovement <= 0)
        {
            distance = Vector3.right * (direction * enemyTracker.currentSpeed * settings.movementUnitConversionRate * Time.deltaTime);
            leftBoundary.position += distance;
            rightBoundary.position += distance;
        }
        else
        {
            distance = Vector3.down * (enemyTracker.currentSpeed * settings.downwardMovementSpeedMultiplier * settings.movementUnitConversionRate * Time.deltaTime);
            remainingDownwardMovement += distance.y; // it's a negative value, so add it instead of subtracting
        }
        foreach (var enemy in enemyTracker.enemies)
        {
            enemy.transform.position += distance;
        }
    }

    private void ReverseDirection()
    {
        direction *= -1;
        remainingDownwardMovement = settings.rowHeight * settings.movementUnitConversionRate;
    }
}
