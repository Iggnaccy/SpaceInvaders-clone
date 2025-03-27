using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private Pause pause; // to disable pause when player dies
    [SerializeField] private GameOver gameOver;
    private const string EnemyTag = "Enemy";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(EnemyTag))
        {
            pause.canPause = false;
            gameOver.Show(false);
        }
    }
}
