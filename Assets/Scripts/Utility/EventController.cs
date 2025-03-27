using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventController : MonoBehaviour
{
    public UnityEvent<EnemyStatsSO, Enemy> OnEnemyDeath;
    public UnityEvent OnBulletFired;
    public UnityEvent OnWallContact;

    public void TriggerOnEnemyDeath(EnemyStatsSO enemyStatsSO, Enemy enemy)
    {
        OnEnemyDeath.Invoke(enemyStatsSO, enemy);
    }

    public void TriggerOnBulletFired()
    {
        OnBulletFired.Invoke();
    }

    public void TriggerOnWallContact()
    {
        OnWallContact.Invoke();
    }
}
