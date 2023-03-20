using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static Enemy;
using static PlayerManager;

public class EnemyFlyingShooter : EnemySentient
{
    [Header("Shooter")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform leftPoint, rightPoint;
    private bool attacking = false;

    private void OnEnable()
    {
        playerTransform = GameObject.Find("PlayerSprite").transform;
        currentState = EnemyAnimationState.idle;
    }

    protected override void CalculateEnemyState()
    {
        Patrol();
        float dir = -Mathf.Sign(gameObject.transform.position.x - playerTransform.position.x);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(dir, 0), Mathf.Infinity, targetLayerMask);
        if (hit)
        {
            attacking = true;
            enemySpriteRenderer.flipX = !(playerTransform.position.x <= transform.position.x);
        }
    }

    protected override void Patrol()
    {
        switch (currentState)
        {
            case EnemyAnimationState.idle:
                enemyAnimator.SetInteger("AnimationState", (int)EnemyAnimationState.idle);
                currentRestTimer -= Time.deltaTime;
                if ((currentRestTimer <= 0f) && attacking)
                {
                    currentState = EnemyAnimationState.attack;
                    attacking = false;
                    currentRestTimer = restTimer;
                }
                enemyRB.velocity = new Vector2(0, speed) * (int)patrolDir;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, (int)patrolDir), Mathf.Infinity, patrolLimiter);
                if (hit.distance < 0.5f)
                {
                    patrolDir = (Direction)(-(int)patrolDir); // invert
                }
                break;
            case EnemyAnimationState.attack:
                enemyAnimator.SetInteger("AnimationState", (int)EnemyAnimationState.attack);
                currentState = EnemyAnimationState.idle;
                break;
            default: break;
        }
    }

    public void Shoot()
    {
        GameObject obj = Instantiate(projectile);
        if (enemySpriteRenderer.flipX) // right
        {
            obj.GetComponent<SpriteRenderer>().flipX = true;
            obj.transform.position = rightPoint.position;
            obj.GetComponent<Projectile>().direction = new Vector3(1, 0, 0);
        }
        else //left
        {
            obj.transform.position = leftPoint.position;
            obj.GetComponent<Projectile>().direction = new Vector3(-1, 0, 0);
        }
        obj.SetActive(true);
    }

    protected override void HitReact(Transform obj)
    {
        enemyAnimator.SetInteger("AnimationState", (int)EnemyAnimationState.hurt);
        Vector2 dir = new Vector2(Mathf.Sign(gameObject.transform.position.x - obj.position.x), 0);
        enemyRB.velocity = dir * 2f;
        if (enemyRB.velocity.x > 0)
        {
            enemySpriteRenderer.flipX = false;
        }
        else
        {
            enemySpriteRenderer.flipX = true;
        }
    }

}
