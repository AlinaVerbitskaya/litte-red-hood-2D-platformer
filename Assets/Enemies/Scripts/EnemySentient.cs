using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemySentient : Enemy
{
    [Header("Target")]
    [SerializeField] protected LayerMask targetLayerMask;
    protected Transform playerTransform;

    [Header("Patrol")]
    [SerializeField] protected Direction patrolDir = Direction.left;
    [SerializeField, Range(0.01f, 20f)] protected float speed = 2f;
    [SerializeField] protected LayerMask patrolLimiter;
    [SerializeField, Range(0.01f, 20f)] protected float restTimer = 2f;
    protected float currentRestTimer = 2f;

    protected EnemyAnimationState currentState;

    private void OnEnable()
    {
        playerTransform = GameObject.Find("PlayerSprite").transform;
        currentState = EnemyAnimationState.idle;
    }

    protected override void CalculateEnemyState()
    {
        Patrol();
        bool seesPlayer = Physics2D.OverlapCircle(transform.position, 2f, targetLayerMask);
        if (seesPlayer)
        {
            enemySpriteRenderer.flipX = (playerTransform.position.x <= transform.position.x);
            enemyAnimator.SetInteger("AnimationState", (int)EnemyAnimationState.attack);
            currentState = EnemyAnimationState.attack;
        }
    }

    protected virtual void Patrol()
    {
        switch (currentState)
        {
            case EnemyAnimationState.idle:
                enemyAnimator.SetInteger("AnimationState", (int)EnemyAnimationState.idle);
                currentRestTimer -= Time.deltaTime;
                if (currentRestTimer <= 0f)
                {
                    patrolDir = (Direction)(-(int)patrolDir); // invert
                    enemySpriteRenderer.flipX = !enemySpriteRenderer.flipX;
                    currentState = EnemyAnimationState.run;
                }
                break;
            case EnemyAnimationState.run:
                enemyAnimator.SetInteger("AnimationState", (int)EnemyAnimationState.run);
                patrolDir = enemySpriteRenderer.flipX ? Direction.left : Direction.right;
                enemyRB.velocity = new Vector2(speed, 0) * (int)patrolDir;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2((int)patrolDir, 0), Mathf.Infinity, patrolLimiter);
                if (hit.distance < 0.5f)
                {
                    currentRestTimer = restTimer;
                    currentState = EnemyAnimationState.idle;
                }
                break;
            case EnemyAnimationState.hurt:
                currentRestTimer = restTimer;
                currentState = EnemyAnimationState.idle;
                break;
            case EnemyAnimationState.attack:
                enemyRB.velocity = new Vector2(0, enemyRB.velocity.y);
                currentRestTimer = restTimer;
                currentState = EnemyAnimationState.idle;
                break;
            case EnemyAnimationState.death:
                enemyRB.bodyType = RigidbodyType2D.Kinematic;
                break;
            default: break;
        }
    }
}
