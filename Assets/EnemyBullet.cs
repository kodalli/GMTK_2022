using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [SerializeField]
    private float lifeSpan = 20f;

    private void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    private void Destroy() => Destroy(gameObject);

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player == null) return;
        var playerDamage = GameManager.Instance.playerEffects.damageBoost;
        player.TakeDamage(playerDamage);
        gameObject.SetActive(false);
    }
}
