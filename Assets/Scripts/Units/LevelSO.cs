using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "SO/Level")]
public class LevelSO : ScriptableObject
{
    public List<LevelRow> rows = new List<LevelRow>();
}

[System.Serializable]
public class LevelRow
{
    [Tooltip("Left to right")]
    public List<Enemy> enemies;
}
