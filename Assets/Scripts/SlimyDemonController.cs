using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimyDemonController : MonoBehaviour {
    [SerializeField] private float speed = 2f;
    [SerializeField] private List<Transform> waypoints;

    private int waypointIndex;
    private bool inRoutine;
    void Update() {
        if (!inRoutine) {
            inRoutine = true;
            StartCoroutine(MoveEnemy());
        }
    }

    IEnumerator MoveEnemy() {

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position,
            waypoints[waypointIndex].transform.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, waypoints[waypointIndex].transform.position) < 0.001f) {
            waypointIndex += 1;
            yield return new WaitForSeconds(2f);
        }

        if (waypointIndex == waypoints.Count) {
            waypointIndex = 0;
        }
        inRoutine = false;
    }
}