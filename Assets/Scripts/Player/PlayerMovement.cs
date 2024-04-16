using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum MovementState {
        WALK,
        AIR
    }

    [Header("Movement")]
    public float walkSpeed;
    public float groundDrag;
    [HideInInspector] public MovementState movementState;
    private float moveSpeed;

    [Header("Ground Check")]
    public float playerHeight;
    bool grounded;

    [Header("Slope Check")]
    public float maxSlopeAngle;
    RaycastHit slopeHit;
    bool exitingSlope;

    Rigidbody rb;
    Vector3 moveDirection;
    float horizontalInput, verticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Check if is grounded
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);
        exitingSlope = !grounded;

        if (!BookUIManager.Instance.showingBook) {
            GetInput();
            SpeedControl();
            SetDrag();
            HandleMovementState();
        }
    }

    void FixedUpdate() {
        if (!BookUIManager.Instance.showingBook) {
            Move();
        }
    }

    void GetInput() {
        if (GameManager.Instance.InputIsAvailable()) {
            Vector2 movement = InputController.Instance.GetWalkDirection();
            horizontalInput = movement.x;
            verticalInput = movement.y;
        }
    }

    void HandleMovementState() {
        if (!grounded) {
            movementState = MovementState.AIR;
        }

        else {
            movementState = MovementState.WALK;
            moveSpeed = walkSpeed;
        }
    }

    void Move() {
        moveDirection = (transform.right * horizontalInput + transform.forward * verticalInput).normalized;

        // Apply force perpendicular to slope's normal if on slope
        if (OnSlope() && !exitingSlope) {
            rb.AddForce(20 * moveSpeed * GetSlopeMoveDirection(), ForceMode.Force);

            // Apply downward force to keep player on slope
            if (rb.velocity.y > 0) {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        // Move in direction
        else if (grounded) {
            rb.AddForce(10 * moveSpeed * moveDirection, ForceMode.Force);
        }

        // Disable gravity while on slope to avoid slipping
        rb.useGravity = !OnSlope();
    }

    
    void SetDrag()
    {
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    void SpeedControl() {

        // Prevents player from exceeding move speed on slopes
        if (OnSlope() && !exitingSlope && rb.velocity.magnitude > moveSpeed) {
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        else {
            Vector3 rawVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            // Clamp x and z axis velocity
            if (rawVelocity.magnitude > moveSpeed) {
                Vector3 clampedVelocity = rawVelocity.normalized * moveSpeed;
                rb.velocity = new Vector3(clampedVelocity.x, rb.velocity.y, clampedVelocity.z);
            }
        }
    }

    bool OnSlope() {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f)) {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);

            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    Vector3 GetSlopeMoveDirection() {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    public Vector3 GetMoveVelocity() {
        return new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }
    
    public bool isGrounded() {
        return grounded;
    }

    public MovementState GetMovementState() {
        return movementState;
    }
}
