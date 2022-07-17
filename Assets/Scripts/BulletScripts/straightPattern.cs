using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class straightPattern : MonoBehaviour
{
    public GameObject homingProj1;

    [SerializeField]
    private int projs = 3;
    void Start()
    {
        StartCoroutine(Spawn(projs));
    } 
    private IEnumerator Spawn(int projs)
    {
        for (int i = 0; i < projs; i++)
        {

            Instantiate(homingProj1, transform.position,transform.rotation);
            yield return new WaitForSeconds(.5f);
        }
    }
}
 
