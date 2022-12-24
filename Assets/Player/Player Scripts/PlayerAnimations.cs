using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public enum PlayerAnimationState {idle = 0, running = 1, jumping = 2, hurt = 3, shooting = 4, dead = 5, fall = 6};

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private Animator playerAnimator;
    [HideInInspector] public PlayerManager.Direction spriteDir;
    private bool inAir = false;


    private void OnEnable()
    {
        spriteDir = PlayerManager.Direction.right;
    }

    public PlayerManager.Direction UpdatePlayerDirection(float horizontalAxis)
    {
        if (horizontalAxis >= 0)
        {
            spriteDir = PlayerManager.Direction.right;
            playerSpriteRenderer.flipX = false;
        }
        else
        {
            spriteDir = PlayerManager.Direction.left;
            playerSpriteRenderer.flipX = true;
        }
        return spriteDir;
    }

    public void UpdateAnimationState(PlayerAnimationState newState)
    {
        if (newState != GetCurrentState())
        {
            playerAnimator.SetInteger("AnimationState", (int)newState);
        }
    }

    public PlayerAnimationState GetCurrentState()
    {
        return (PlayerAnimationState)playerAnimator.GetInteger("AnimationState");
    }

    public void InAirToggle(bool isInAir)
    {
        inAir = isInAir;
    }

    public bool IsInAir()
    {
        return inAir;
    }

    public IEnumerator Flicker(float seconds)
    {
        playerSpriteRenderer.color = new Color(1f, 0.25f, 0.25f, 0.6f);
        yield return new WaitForSeconds(seconds);
        playerSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    public PlayerManager.Direction GetPlayerDirection()
    {
        return spriteDir;
    }
}
