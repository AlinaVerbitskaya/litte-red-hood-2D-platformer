using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

[RequireComponent(typeof(PlayerInputs))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerManager plManager;

    [Header("Speed")]
    [SerializeField, Range(0f, 10f)] float playerSpeed = 2f;
    [SerializeField, Range(0f, 20f)] float jumpForce = 4f;
    [SerializeField] AnimationCurve SpeedCurve;
    [SerializeField] private Rigidbody2D playerRigidBody;

    [Header("Grounded check")]
    [SerializeField] private Transform groundColliderTransform;    
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float groundedCheckRadius = 0.06f;
    private bool touchingGround = false, closeToGround = false, jumpPressed = false;

    void FixedUpdate()
    {
        CloseToGroundCheck();
    }

    public void Move(float horizontalAxis)
    {
        playerRigidBody.velocity = new Vector2(SpeedCurve.Evaluate(horizontalAxis) * playerSpeed, playerRigidBody.velocity.y);
    }

    public void RegisterJump()
    {
        jumpPressed = true;
    }

    private void Jump()
    {
        if (jumpPressed)
        {
            Vector2 newVelocity = new Vector2(playerRigidBody.velocity.x, jumpForce);
            playerRigidBody.velocity = newVelocity;
            jumpPressed = false;
            plManager.Jumping();
        }
    }

    private void CloseToGroundCheck()
    {
        Vector3 overlapCirclePosition = groundColliderTransform.position;
        Vector2 capsuleSize = new Vector2(0.1f, 1f);
        closeToGround = Physics2D.OverlapCapsule(overlapCirclePosition + new Vector3(0, -0.1f, 0), capsuleSize, CapsuleDirection2D.Vertical, 0, layerMask);
        if (!closeToGround)
        {
            jumpPressed = false;
        }
        touchingGround = Physics2D.OverlapCircle(overlapCirclePosition, groundedCheckRadius, layerMask);
        if (touchingGround) Jump();
    }

    public void Push(float force, Transform obj)
    {
        Vector2 dir = new Vector2(Mathf.Sign(gameObject.transform.position.x - obj.position.x), 1);
        dir.Normalize();
        playerRigidBody.velocity = dir * force;
    }

    public bool IsTouchingGround()
    {
        return touchingGround;
    }

    public Vector2 GetPlayerVelocity()
    {
        return playerRigidBody.velocity;
    }
}
