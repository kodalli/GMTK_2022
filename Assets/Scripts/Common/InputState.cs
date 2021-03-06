using System;
using UnityEngine;

[Serializable]
public struct InputState {
    public Vector2 movementDirection;
    public Vector2 mouseDirection;

    public bool interactClicked;
    public bool interactReleased;
    public bool isInteracting;
    public float holdTimer;

    public bool leftMouseButtonClicked;
    public bool leftMouseButtonReleased;
}