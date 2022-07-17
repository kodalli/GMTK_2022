using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    public float time = 15f;


    void Start()
    {
        Destroy(this.gameObject,time);    
    }

}
