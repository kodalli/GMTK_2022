using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy {
    void TakeDamage();
}
public class SlimyDemonController : MonoBehaviour, IEnemy {
    private SpriteRenderer SpriteRenderer;
    
    [SerializeField] private float health = 1000;
    [SerializeField] private float speed = 6f;
    [SerializeField] private List<Transform> waypoints;

    private int waypointIndex;
    private bool inRoutine;
    void Start()
    {
        health = 1000;
        transform.position = waypoints[0].transform.position;
    }
    void Update() {
         if (!inRoutine) {
             inRoutine = true;
             StartCoroutine(MoveEnemy());
         }
    }

    public void TakeDamage() {
        Debug.Log(health);
        health -= 10;
        
        if (health == 0) {
            Destroy(this.gameObject);
        }
    }
    IEnumerator MoveEnemy() {

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position,
            waypoints[waypointIndex].transform.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, waypoints[waypointIndex].transform.position) < 0.001f) {
            waypointIndex += 1;
            yield return new WaitForSeconds(8f);
        }

        if (waypointIndex == waypoints.Count) {
            waypointIndex = 0;
        }
        inRoutine = false;
    }
}