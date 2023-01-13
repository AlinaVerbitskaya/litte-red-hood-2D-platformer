using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum Direction { left = -1, right = 1 };

    [SerializeField] private SpriteRenderer plSpriteRenderer;
    [SerializeField] private PlayerInputs plInputs;
    [SerializeField] private PlayerMovement plMovement;
    [SerializeField] private PlayerAnimations plAnimations;
    [SerializeField] private Health plHealth;

    [Header("Shooting")]
    [SerializeField] private Shooter plShooter;
    [SerializeField] private Transform leftShootingPoint, rightShootingPoint;

    private void LateUpdate()
    {
        CalculatePlayerState();
    }

    public void Move(float horizontalAxis)
    {
        plMovement.Move(horizontalAxis);
        plAnimations.UpdatePlayerDirection(horizontalAxis);
    }

    public void CalculatePlayerState()
    {
        Vector2 velocity = plMovement.GetPlayerVelocity();
        if ((velocity.magnitude <= 0.5f) || (plMovement.IsTouchingGround() && (plInputs.isMoving() == 0)))
        {
            plAnimations.UpdateAnimationState(PlayerAnimations.PlayerAnimationState.idle);
        }
        else if ((velocity.y <= -0.2f) && !plMovement.IsTouchingGround())
        {
            plAnimations.UpdateAnimationState(PlayerAnimations.PlayerAnimationState.fall);
        }
        else if ((plInputs.isMoving() != 0) && plMovement.IsTouchingGround())
        {
            plAnimations.UpdateAnimationState(PlayerAnimations.PlayerAnimationState.running);
        }
    }

    public void Shoot()
    {
        plAnimations.UpdateAnimationState(PlayerAnimations.PlayerAnimationState.shooting);
    }

    private void SpawnArrow()
    {
        Vector2 dir;
        Vector3 point;
        if (plAnimations.GetPlayerDirection() == Direction.left)
        {
            dir = new Vector2(-1f, 0f);
            point = leftShootingPoint.position;
        }
        else
        {
            dir = new Vector2(1f, 0f);
            point = rightShootingPoint.position;
        }
        plShooter.Shoot(dir, point);
    }

    public void Jumping()
    {
        plAnimations.UpdateAnimationState(PlayerAnimations.PlayerAnimationState.jumping);
    }

    public void RegisterJump()
    {
        plMovement.RegisterJump();
    }

    public void HitReact(Transform obj)
    {
        if (!plHealth.IsInvincible())
        {
            StartCoroutine(plInputs.PlayerInputsPause(0.3f));
            StartCoroutine(plAnimations.Flicker(1f));
            StartCoroutine(plHealth.MakeInvincible(1f));
            plAnimations.UpdateAnimationState(PlayerAnimations.PlayerAnimationState.hurt);
            plMovement.Push(6f, obj);
        }
    }

    private void Death()
    {
        EventManager.OnPlayerDeath?.Invoke();
        plInputs.TogglePlayerInputs(false);
        plHealth.invincible = true;
        plAnimations.UpdateAnimationState(PlayerAnimations.PlayerAnimationState.dead);
    }

    public void Teleport(Vector3 position)
    {
        if (plHealth.CurrentHealth > 0)
        {
            GetComponent<Transform>().position = position;
        }
    }

    private void DeathTriggerReact()
    {
        EventManager.OnPlayerInDeathTrigger?.Invoke();
    }
}
