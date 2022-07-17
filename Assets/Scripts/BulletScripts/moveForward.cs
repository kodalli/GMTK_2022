using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveForward : MonoBehaviour
{
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private Vector2 Dir = new Vector2(-1f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody2D>().AddForce(Dir*speed,ForceMode2D.Impulse);
    }
}
