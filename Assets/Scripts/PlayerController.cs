using System;
using System.Collections;
using System.Collections.Generic;
using Aarthificial.Reanimation;
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
    [SerializeField] private float runSpeed = 10;
    [SerializeField] private float walkSpeed = 7;
    [SerializeField] private Stopwatch runStopwatch = new Stopwatch();

    private InputState inputState => inputProvider;
    private Vector2 MovementDirection => inputState.movementDirection;
    private Vector2 MouseDirection => inputState.mouseDirection;


    private Reanimator reanimator;
    private CollisionDetection collisionDetection;
    private GameObject gun;
    private Transform firePoint;


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
        firePoint = transform.Find("firePoint");
    }

    private void OnEnable() {
        //inputProvider.JumpEvent += OnRun;
        inputProvider.MousePosEvent += OnMouse;
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
        //inputProvider.JumpEvent -= OnRun;
        inputProvider.MousePosEvent -= OnMouse;
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

    private void OnMouse(Vector2 value) =>
        gunDirectionInput = Camera.main.ScreenToWorldPoint(value) - transform.position;

    public void EnterMovementState() {
        State = PlayerState.Movement;
    }

    private void OnRun() => EnterRunState();

    private void EnterRunState() {
        if (State != PlayerState.Movement || !runStopwatch.IsReady) return;
        State = PlayerState.Run;

        runStopwatch.Split();
    }

    private void UpdateGunDirection() {
        if (gunDirectionInput != Vector2.zero) {
            gunDirection = gunDirectionInput;
            gunDirection.Normalize();
        }

        float angle = Vector2.SignedAngle(Vector2.right, gunDirection);
        gun.transform.rotation = Quaternion.Euler(0f, 0f, angle + 270);

        if (movingLeft) {
            gun.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            gun.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
        }
        if (movingRight) {
            gun.gameObject.GetComponent<SpriteRenderer>().flipY = false;
        }

    }

    private void UpdateMovementState() {
        var previousVelocity = collisionDetection.rigidbody2D.velocity;
        var velocityChange = Vector2.zero;

        velocityChange.x = (MovementDirection.x * walkSpeed - previousVelocity.x) / 4;
        velocityChange.y = (MovementDirection.y * walkSpeed - previousVelocity.y) / 4;

        if (State == PlayerState.Run) {
            velocityChange.x = (MovementDirection.x * runSpeed - previousVelocity.x) / 4;
            velocityChange.y = (MovementDirection.y * runSpeed - previousVelocity.y) / 4;

            if (runStopwatch.IsFinished || collisionDetection.wallContact.HasValue) {
                runStopwatch.Split();
                EnterMovementState();
            }
        }

        if (collisionDetection.wallContact.HasValue) {
            var wallDirection = (int) Mathf.Sign(collisionDetection.wallContact.Value.point.x - transform.position.x);
            var walkDirection = (int) Mathf.Sign(MovementDirection.x);

            if (walkDirection == wallDirection)
                velocityChange.x = 0;
        }

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