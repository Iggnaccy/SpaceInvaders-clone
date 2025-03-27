using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    [SerializeField] private SimpleSettingsSO settings;
    [SerializeField] private EventController eventController;
    private static float _lastHitTime = -100;
    private const string EnemyTag = "Enemy";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(EnemyTag) && Time.time - _lastHitTime > settings.downMovementTimeout)
        {
            eventController.TriggerOnWallContact();
            _lastHitTime = Time.time;
        }
    }
}
