using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Direction { left = -1, right = 1 };
    public enum EnemyAnimationState { idle = 0, run = 1, hurt = 2, death = 3, attack = 4 };

    [SerializeField] protected SpriteRenderer enemySpriteRenderer;
    [SerializeField] protected Animator enemyAnimator;
    [SerializeField] protected Rigidbody2D enemyRB;
    [SerializeField] protected Health enemyHealth;
    [SerializeField] protected Collider2D damagingCollider;

    protected void LateUpdate()
    {
        CalculateEnemyState();
        //AnimationUpdate();
    }

    protected void AnimationUpdate()
    {
        //enemyAnimator.SetInteger("AnimationState", (int)currentState);
    }

    protected virtual void CalculateEnemyState()
    {
        if (enemyRB.velocity.magnitude < 0.5f)
        {
            enemyAnimator.SetInteger("AnimationState", (int)EnemyAnimationState.idle);
        }
    }

    protected virtual void HitReact(Transform obj)
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

    protected void DeathTriggerReact()
    {
        enemyHealth.ChangeHealth(-enemyHealth.CurrentHealth);
    }

    protected void Death()
    {
        enemyRB.bodyType = RigidbodyType2D.Kinematic;
        enemyAnimator.SetInteger("AnimationState", (int)EnemyAnimationState.death);
        enemyHealth.invincible = true;
        if (damagingCollider != null) damagingCollider.enabled = false;
        Destroy(gameObject, 2.5f);
    }
}