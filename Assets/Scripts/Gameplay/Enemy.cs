using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ObjectPoolManagerSO poolManager;
    public EnemyStatsSO baseStats;
    public int hp;

    private EventController _eventController;

    public void Setup(EventController eventController)
    {
        hp = baseStats.hp;
        _eventController = eventController;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            _eventController.TriggerOnEnemyDeath(baseStats, this);
            poolManager.ReturnToPool(gameObject);
        }
    }
}
