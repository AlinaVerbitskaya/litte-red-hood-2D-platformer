using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Direction { left = -1, right = 1 };
    public enum EnemyAnimationState { idle = 0, run = 1, hurt = 2, death = 3 };

    [SerializeField] private SpriteRenderer enemySpriteRenderer;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private Rigidbody2D enemyRB;
    [SerializeField] private Health enemyHealth;

    private void LateUpdate()
    {
        CalculateEnemyState();
    }

    private void CalculateEnemyState()
    {
        if (enemyRB.velocity.magnitude < 0.5f)
        {
            enemyAnimator.SetInteger("AnimationState", (int)EnemyAnimationState.idle);
        }
    }

    private void HitReact(Transform obj)
    {
        enemyAnimator.SetInteger("AnimationState", (int)EnemyAnimationState.hurt);
        Vector2 dir = new Vector2(Mathf.Sign(gameObject.transform.position.x - obj.position.x), 1);
        dir.Normalize();
        enemyRB.velocity = dir * 2f;
        if (enemyRB.velocity.x > 0)
        {
            enemySpriteRenderer.flipX = true;
        }
        else
        {
            enemySpriteRenderer.flipX = false;
        }
    }

    private void Death()
    {
        enemyAnimator.SetInteger("AnimationState", (int)EnemyAnimationState.death);
        enemyHealth.invincible = true;
        Destroy(gameObject, 2.5f);
    }
}