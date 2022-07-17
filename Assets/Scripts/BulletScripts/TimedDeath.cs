using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeath : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan = 20f;
    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }


}
