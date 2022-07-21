using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMoveTogether : MonoBehaviour
{
    public GameObject TrianglesPrefab;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("TriangleSummon",0f,2f);
    }
    private void TriangleSummon()
    {
        var position = transform.position;
        var face = (player.transform.position - position).normalized;
        //GameObject bullet =
        var right = new Vector3(position.x+.5f, position.y-.4f,position.z);
        Instantiate(TrianglesPrefab, right,Quaternion.Euler(0f,0f,-90f));
        
      //  bullet.transform.position 
        //bullet.GetComponent<Rigidbody2D>().AddForce(TriangleMoveDir * projectileSpeed, ForceMode2D.Impulse);
    }


}
