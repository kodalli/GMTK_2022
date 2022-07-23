using System;
using System.Collections;
using System.Collections.Generic;
using Card;
using UnityEngine;

public interface IEnemy {
    void TakeDamage(float damageValue);
}

public class SlimyDemonController : MonoBehaviour, IEnemy {
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float health = 100;
    [SerializeField] private float speed = 6f;
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private Material takeDamageMat;
    [SerializeField] private Material spritesDefault;
    
    public int damageBoost = 5;
    public int fireRate = 100;
    public int durability = 1;

    private int waypointIndex;
    private bool inRoutine;

    private void Start() {
        health = 1000;
        transform.position = waypoints[0].transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        ApplyEffects();
    }

    private void Update() {
        if (inRoutine) return;
        inRoutine = true;
        StartCoroutine(MoveEnemy());
    }

    // TODO implement effects
    private void ApplyEffects() {
        StatusEffects.ApplyEnemy(ref damageBoost, ref fireRate, ref durability);
        GameManager.Instance.SetEnemyEffects(damageBoost, fireRate, durability);
        Debug.Log(
            $"Enemy: damage: {damageBoost}, fire rate: {fireRate}, durability: {durability}");
    }

    public void TakeDamage(float damageValue) {
        // TODO: damage adjust from status effect
        health -= StatusEffects.GetFactor(damageValue, durability);
        DamageAnimation();
        
        if (health <= 0) {
            GameManager.Instance.LoadMainMenu();
        }
    }
    
    private void DamageAnimation() {
        StartCoroutine(FlashDamage());
    }

    private IEnumerator FlashDamage() {
        spriteRenderer.material = takeDamageMat;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = spritesDefault;
    }

    private IEnumerator MoveEnemy() {
        var step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position,
            waypoints[waypointIndex].transform.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, waypoints[waypointIndex].transform.position) <
            0.001f) {
            waypointIndex += 1;
            yield return new WaitForSeconds(8f);
        }

        if (waypointIndex == waypoints.Count) {
            waypointIndex = 0;
        }

        inRoutine = false;
    }
}