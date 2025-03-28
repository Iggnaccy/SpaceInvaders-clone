using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private PlayerStatsSO playerStats;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint, bulletParent;

    [SerializeField, ReadOnly] private float lastAttackTime = -10;

    private void Start()
    {
        attackAction.action.performed += Attack;
    }

    private void OnDestroy()
    {
        attackAction.action.performed -= Attack;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if(Time.time - lastAttackTime < playerStats.fireRate || Time.timeScale == 0)
        {
            return;
        }
        lastAttackTime = Time.time;
        var bullet = bulletPrefab.poolManager.Get(bulletParent).GetComponent<Bullet>();
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.Setup(playerStats.shotDamage, playerStats.bulletSpeed);
    }
}
