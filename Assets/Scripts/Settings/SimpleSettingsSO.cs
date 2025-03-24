using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Simple Settings", menuName = "SO/Simple Settings")]
public class SimpleSettingsSO : ScriptableObject
{
    public float movementUnitConversionRate;
    // example future settings if game was to be developed further:
    // public bool musicEnabled; 
    // public float musicVolume;
    // public bool soundEnabled;
    // public float soundVolume;
    // selected level set
    // etc.
}
