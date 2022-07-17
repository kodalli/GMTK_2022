using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [SerializeField]
    private float lifeSpan = 20f;
    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    private void Destroy() => Destroy(gameObject);

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage();
            gameObject.SetActive(false);
        }
    }
}
