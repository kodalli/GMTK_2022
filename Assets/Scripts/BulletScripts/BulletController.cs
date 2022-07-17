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
    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    private void Destroy() => Destroy(gameObject);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEnemyBullet)
        {
            var enemy = other.gameObject.GetComponent<IEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage();
                Destroy();
            }
        }
        else
        {
            var player = other.gameObject.GetComponent<IPlayer>();
            if (player != null)
            {
                player.TakeDamage();
                gameObject.SetActive(false);
            }

        }
    }
}
