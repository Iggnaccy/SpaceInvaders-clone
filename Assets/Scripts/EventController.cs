using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventController : MonoBehaviour
{
    public UnityEvent<EnemyStatsSO> OnEnemyDeath;
    public UnityEvent OnBulletFired;

    public void TriggerOnEnemyDeath(EnemyStatsSO enemyStatsSO)
    {
        OnEnemyDeath.Invoke(enemyStatsSO);
    }

    public void TriggerOnBulletFired()
    {
        OnBulletFired.Invoke();
    }
}
