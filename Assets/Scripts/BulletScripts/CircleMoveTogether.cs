using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMoveTogether : MonoBehaviour
{
    public GameObject TrianglesPrefab;

    [SerializeField]
    private Vector2 TriangleMoveDir = new Vector2(4f,1f);

    [SerializeField]    
    private float projectileSpeed = 4f;

    void Start()
    {
        InvokeRepeating("TriangleSummon",0f,1f);
    }
    private void TriangleSummon()
    {
        GameObject bullet = Instantiate(TrianglesPrefab, transform);
      //  bullet.transform.position 
        bullet.GetComponent<Rigidbody2D>().AddForce(TriangleMoveDir * projectileSpeed, ForceMode2D.Impulse);
        

        Destroy(bullet, 4f);

        
    }
}
