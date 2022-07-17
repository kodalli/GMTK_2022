using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aarthificial.Reanimation;
using Card;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public enum PlayerState {
    Movement = 0,
    Run = 1,
    Hit = 2,
}

public interface IPlayer {
    void TakeDamage(float damageValue);
}

public class PlayerController : MonoBehaviour, IPlayer {
    [FormerlySerializedAs("PlayerHealth")] [SerializeField]
    public int playerHealth = 200;

    private static class Drivers {
        public const string IsMoving = "isMoving";
        public const string IsMovingHorizontal = "isMovingHorizontal";
        public const string IsMovingRight = "isMovingRight";
        public const string IsMovingUp = "isMovingUp";

        public const string MovingRight = "movingRight";
        public const string MovingLeft = "movingLeft";
    }


    [SerializeField] private InputProvider inputProvider;
    [SerializeField] private InteractionLogic interactionLogic;
    [SerializeField] private new BoxCollider2D collider;
    [SerializeField] private float walkSpeed = 7;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private Material takeDamageMat;
    [SerializeField] private Material spritesDefault;
    private readonly Stopwatch runStopwatch = new Stopwatch();

    private InputState inputState => inputProvider;
    public Vector2 MovementDirection => inputState.movementDirection;
    private Vector2 MouseDirection => inputState.mouseDirection;
    public InputProvider InputProvider => inputProvider;


    private Reanimator reanimator;
    private CollisionDetection collisionDetection;
    private GameObject gun;
    private SpriteRenderer spriteRenderer;
    public Transform firePoint;
    public GameObject bulletPrefab;


    public PlayerState State { get; set; } = PlayerState.Movement;
    private Vector2 gunDirectionInput;

    private Vector2 gunDirection;
    // private bool movingRight;
    // private bool movingLeft;

    private int durability = 1;
    private int fireRate = 100;
    private int damageBoost = 5;
    private bool fire = false;

    [SerializeField] private float fireCountDown = 0;

    private void Awake() {
        inputProvider.EnableInput();

        reanimator = GetComponent<Reanimator>();
        collisionDetection = GetComponent<CollisionDetection>();
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        gun = transform.Find("Gun").gameObject;

    }

    private void Start() {
        ApplyEffects();
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(collider.bounds.center, 2f);
    }

    private void OnEnable() {
        inputProvider.MousePosEvent += OnMouse;
        inputProvider.ShootEventStart += ToggleFireOn;
        inputProvider.ShootEventEnd += ToggleFireOff;
        // reanimator.AddListener(Drivers.MovingLeft, () => {
        // movingLeft = true;
        // movingRight = false;
        // });
        // reanimator.AddListener(Drivers.MovingRight, () => {
        // movingRight = true;
        // movingLeft = false;
        // });
    }

    private void OnDisable() {
        inputProvider.MousePosEvent -= OnMouse;
        inputProvider.ShootEventStart -= ToggleFireOn;
        inputProvider.ShootEventEnd -= ToggleFireOff;

        // reanimator.RemoveListener(Drivers.MovingLeft, () => {
        // movingLeft = true;
        // movingRight = false;
        // });
        // reanimator.RemoveListener(Drivers.MovingRight, () => {
        // movingRight = true;
        // movingLeft = false;
        // });
    }

    private void Update() {
        UpdateFireGun();
        UpdateMovementState();
        UpdateGunDirection();
        UpdateGunDirection();
        interactionLogic.UpdateInteractable(this, collider.bounds.center);

        reanimator.Set(Drivers.IsMoving, MovementDirection != Vector2.zero);
        reanimator.Set(Drivers.IsMovingHorizontal, MovementDirection.x != 0);
        reanimator.Set(Drivers.IsMovingRight, MovementDirection.x > 0);
        reanimator.Set(Drivers.IsMovingUp, MovementDirection.y > 0);
    }

    #region Status Effects

    private void ApplyEffects() {
        StatusEffects.ApplyPlayer(ref damageBoost, ref fireRate, ref durability);
        GameManager.Instance.SetPlayerEffects(damageBoost, fireRate, durability);
        Debug.Log(
            $"Player: damage: {damageBoost}, fire rate: {fireRate}, durability: {durability}");
    }


    #endregion

    #region Take Damage

    public void TakeDamage(float damageValue) {
        Debug.Log(playerHealth);
        if (playerHealth > 0) {
            DamageAnimation();
            playerHealth -= StatusEffects.GetFactor(damageValue, durability);
        }
        else {
            GameManager.Instance.justDied = true;
            GameManager.LoadCasinoScene1();
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

    #endregion

    #region Shooting

    private void UpdateFireGun() {
        if (fire && fireCountDown < 0) {
            fireCountDown = 10f / fireRate;
            Shoot();
        }

        if (fireCountDown > -0.1f) {
            fireCountDown -= Time.deltaTime;
        }
    }

    private void ToggleFireOn() => fire = true;

    private void ToggleFireOff() => fire = false;

    private void Shoot() {
        if (gun == null || !gun.activeSelf) {
            return;
        }

        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    #endregion


    private void OnMouse(Vector2 value) =>
        gunDirectionInput = Camera.main.ScreenToWorldPoint(value) - transform.position;

    private void UpdateGunDirection() {
        if (gunDirectionInput != Vector2.zero) {
            gunDirection = gunDirectionInput;
            gunDirection.Normalize();
        }

        float angle = Vector2.SignedAngle(Vector2.right, gunDirection);
        gun.transform.rotation = Quaternion.Euler(0f, 0f, angle + 270);
        //firePoint.transform.rotation = gun.transform.rotation;

        // if (!movingRight) {
        //     gun.gameObject.GetComponent<SpriteRenderer>().flipY = true;
        //     gun.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
        //     firePoint.transform.rotation = Quaternion.Euler(0f, 0f, angle + 270);
        // }
        // else {
        //     gun.gameObject.GetComponent<SpriteRenderer>().flipY = false;
        // }
    }

    private void UpdateMovementState() {
        var previousVelocity = collisionDetection.rigidbody2D.velocity;
        var velocityChange = Vector2.zero;

        velocityChange.x = (MovementDirection.x * walkSpeed - previousVelocity.x) / 4;
        velocityChange.y = (MovementDirection.y * walkSpeed - previousVelocity.y) / 4;

        collisionDetection.rigidbody2D.AddForce(velocityChange, ForceMode2D.Impulse);
    }

    public void ToggleInput(bool state) {
        if (state) {
            inputProvider.EnableInput();
        }
        else {
            inputProvider.DisableInput();
        }
    }
}