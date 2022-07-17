using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMoveTogether : MonoBehaviour
{
    public GameObject TrianglesPrefab;

    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("TriangleSummon",0f,2f);
    }
    private void TriangleSummon()
    {
        Vector3 face = (player.transform.position - transform.position).normalized;
        //GameObject bullet =
        Vector3 right = new Vector3(transform.position.x+.5f, transform.position.y-.4f,transform.position.z);
        Instantiate(TrianglesPrefab, right,Quaternion.Euler(0f,0f,-90f));
        
      //  bullet.transform.position 
        //bullet.GetComponent<Rigidbody2D>().AddForce(TriangleMoveDir * projectileSpeed, ForceMode2D.Impulse);
    }


}
