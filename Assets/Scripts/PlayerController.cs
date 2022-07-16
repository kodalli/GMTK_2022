using System;
using System.Collections;
using System.Collections.Generic;
using Aarthificial.Reanimation;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public enum PlayerState {
    Movement = 0,
    Run = 1,
    Hit = 2,
}


public class PlayerController : MonoBehaviour {
    private static class Drivers {
        public const string IsMoving = "isMoving";
        public const string IsMovingHorizontal = "isMovingHorizontal";
        public const string IsMovingRight = "isMovingRight";
        public const string IsMovingUp = "isMovingUp";

        public const string MovingRight = "movingRight";
        public const string MovingLeft = "movingLeft";
    }


    [SerializeField] private InputProvider inputProvider;
    [SerializeField] private float walkSpeed = 7;
    [SerializeField] private float bulletForce = 20f;
    private readonly Stopwatch runStopwatch = new Stopwatch();

    private InputState inputState => inputProvider;
    private Vector2 MovementDirection => inputState.movementDirection;
    private Vector2 MouseDirection => inputState.mouseDirection;


    private Reanimator reanimator;
    private CollisionDetection collisionDetection;
    private GameObject gun;
    public Transform firePoint;
    public GameObject bulletPrefab;


    public PlayerState State { get; set; } = PlayerState.Movement;
    private Vector2 gunDirectionInput;
    private Vector2 gunDirection;
    private bool movingRight;
    private bool movingLeft;

    private void Awake() {
        inputProvider.EnableInput();
        
        reanimator = GetComponent<Reanimator>();
        collisionDetection = GetComponent<CollisionDetection>();

        gun = transform.Find("Gun").gameObject;
    }

    private void OnEnable() {
        inputProvider.MousePosEvent += OnMouse;
        inputProvider.ShootEvent += Shoot;
        reanimator.AddListener(Drivers.MovingLeft, () => {
            movingLeft = true;
            movingRight = false;
        });
        reanimator.AddListener(Drivers.MovingRight, () => {
            movingRight = true;
            movingLeft = false;
        });
    }

    private void OnDisable() {
        inputProvider.MousePosEvent -= OnMouse;
        inputProvider.ShootEvent -= Shoot;
        reanimator.RemoveListener(Drivers.MovingLeft, () => {
            movingLeft = true;
            movingRight = false;
        });
        reanimator.RemoveListener(Drivers.MovingRight, () => {
            movingRight = true;
            movingLeft = false;
        });
    }

    private void Update() {
        UpdateMovementState();
        UpdateGunDirection();

        reanimator.Set(Drivers.IsMoving, MovementDirection != Vector2.zero);
        reanimator.Set(Drivers.IsMovingHorizontal, MovementDirection.x != 0);
        reanimator.Set(Drivers.IsMovingRight, MovementDirection.x > 0);
        reanimator.Set(Drivers.IsMovingUp, MovementDirection.y > 0);
    }

    private void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

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