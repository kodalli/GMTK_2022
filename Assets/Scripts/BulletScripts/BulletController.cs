using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan = 20f;
    
    public bool isEnemyBullet;

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
            var playerDamage = GameManager.Instance.playerEffects.damageBoost;
            enemy.TakeDamage(playerDamage);
            Destroy();
        }
        else
        {
            var player = other.gameObject.GetComponent<IPlayer>();
            if (player == null) return;
            var enemyDamage = GameManager.Instance.enemyEffects.damageBoost;
            player.TakeDamage(enemyDamage);
            Destroy();
        }
    }
}
