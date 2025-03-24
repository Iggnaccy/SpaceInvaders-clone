using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "SO/Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    public float movementSpeed;
    public float shotsPerSecond; // for easier input
    [ReadOnly] public float fireRate; // value used for actual firing logic
    public int shotDamage;
    public float bulletSpeed;

    private void OnValidate()
    {
        if(shotsPerSecond != 0)
        {
            fireRate = 1f / shotsPerSecond;
        }
    }
}
