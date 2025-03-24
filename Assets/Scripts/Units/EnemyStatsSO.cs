using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "SO/Enemy Stats")]
public class EnemyStatsSO : ScriptableObject
{
    public int hp;
    public int minimumSpeed; // entire row moves at the highest minimum speed of any unit within that row
    public int scoreValue;
    public string displayName;
}
