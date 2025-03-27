using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ObjectPoolManagerSO poolManager;
    [SerializeField] private SimpleSettingsSO settings;
    private const string EnemyTag = "Enemy";
    private const string OutOfBoundsTag = "OutOfBounds";

    private int _damage;
    private float _speed;

    private bool alreadyHit;

    public void Setup(int damage, float speed)
    {
        _damage = damage;
        _speed = speed;
        alreadyHit = false;
    }

    private void Update()
    {
        Vector3 distance = Vector3.up * (_speed * Time.deltaTime * settings.movementUnitConversionRate);
        transform.Translate(distance);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(OutOfBoundsTag))
        {
            alreadyHit = false;
            poolManager.ReturnToPool(gameObject);
        }
        if (alreadyHit) return;
        if(other.CompareTag(EnemyTag))
        {
            alreadyHit = true;
            var enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(_damage);
            poolManager.ReturnToPool(gameObject);
        }
    }
}
