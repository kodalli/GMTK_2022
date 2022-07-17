using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan = 20f;

    public bool isEnemyBullet;
    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    private void Destroy() => Destroy(gameObject);

    private void OnTriggerEnter2D(Collider2D other) {
        if (!isEnemyBullet) {
            other.gameObject.GetComponent<IEnemy>()?.TakeDamage();
            Destroy();
            
        }
        else {
            other.gameObject.GetComponent<IPlayer>()?.TakeDamage();
            Destroy();
        }
        
    }
}
