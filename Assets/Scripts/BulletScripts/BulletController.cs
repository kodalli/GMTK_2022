using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan = 20f;
    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    private void Destroy() => Destroy(gameObject);

    private void OnTriggerEnter2D(Collider2D other) {
        var enemy = other.gameObject.GetComponent<IEnemy>();
        if (enemy != null) {
            enemy.TakeDamage();
            Destroy();
        }
    }
}
