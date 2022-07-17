using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingToPlayer : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private float speed = 6f;
    private bool updateOn = true;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Invoke("toPlayer", 3f);
        StartCoroutine(updateOff());
    }

    private void toPlayer()
    {
        Vector2 face =(player.transform.position - transform.position).normalized;
        this.GetComponent<Rigidbody2D>().AddForce(face * speed, ForceMode2D.Impulse);
        Destroy(this, 3f);
    }
    private void Update()
    {
        if (updateOn == true)
        {
            Vector2 face = (player.transform.position - transform.position).normalized;

        }
        
    }
    IEnumerator updateOff()
    {
        yield return new WaitForSeconds(5.0f);
        updateOn = false;
    }
}
