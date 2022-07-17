using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class straightPattern : MonoBehaviour
{
    public GameObject homingProj1;
    [SerializeField]
    public AudioClip pewSound;

    private float timer = 0f;
    public float timerEnd = 10f;

    [SerializeField]
    private int projs = 3;
    private void Update()
    {
        if (timer<timerEnd)
        {
            timer += Time.deltaTime;
        }
        else{
            timer = 0f;
            StartCoroutine(Spawn(projs));
        }
    }
    private IEnumerator Spawn(int projs)
    {
        for (int i = 0; i < projs; i++)
        {
            Instantiate(homingProj1, transform.position,transform.rotation);
            SoundManager.Instance.PlaySound(pewSound);
            yield return new WaitForSeconds(.2f);
        }
    }
}
 
