using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan = 20f;
    [SerializeField]
    public bool isEnemyBullet;

    public float damage = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    private void Destroy() => Destroy(gameObject);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEnemyBullet)
        {
            var enemy = other.gameObject.GetComponent<IEnemy>();
            if (enemy == null) return;
            enemy.TakeDamage(damage);
            Destroy();
        }
        else
        {
            var player = other.gameObject.GetComponent<IPlayer>();
            if (player == null) return;
            player.TakeDamage();
            Destroy();
        }
    }
}
