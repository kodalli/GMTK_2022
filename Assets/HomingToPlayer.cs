using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingToPlayer : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private float speed = 6f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Invoke("toPlayer", (Random.value * 1f) + 8f);
    }//

    private void toPlayer()
    {
        Vector2 face = (player.transform.position - transform.position).normalized;
        this.GetComponent<Rigidbody2D>().AddForce(face * speed, ForceMode2D.Impulse);

    }

}

