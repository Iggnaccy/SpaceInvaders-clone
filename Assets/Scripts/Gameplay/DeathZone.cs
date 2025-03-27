using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private const string EnemyTag = "Enemy";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(EnemyTag))
        {
            // game over
        }
    }
}
