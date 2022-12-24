using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redhood.Inputs;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInputs : MonoBehaviour
{
    [SerializeField] PlayerManager plManager;
    private float horizontal = 0;
    private bool jump = false;
    private bool playerInputsOn = false;
    private bool shoot = false;
    
    void Start()
    {
        TogglePlayerInputs(true);
    }

    void Update()
    {
        GetInputs();
    }

    private void GetInputs()
    {
        if (playerInputsOn)
        {
            horizontal = Input.GetAxis(GlobalStringVars.HORIZONTAL_AXIS);
            jump = Input.GetButtonDown(GlobalStringVars.JUMP_BUTTON);
            shoot = Input.GetButtonDown(GlobalStringVars.ATTACK_BUTTON);
            if (jump)
            {
                plManager.RegisterJump();
            }
            if (isMoving() != 0)
            {
                plManager.Move(horizontal);
            }
            if (shoot)
            {
                plManager.Shoot();
            }
        }
    }

    public int isMoving()
    {
        if (horizontal > 0.01f) return 1;
        else if (horizontal < -0.01f) return -1;
        else return 0;
    }

    public void TogglePlayerInputs(bool toggle)
    {
        playerInputsOn = toggle;
    }

    public IEnumerator PlayerInputsPause(float seconds)
    {
        TogglePlayerInputs(false);
        yield return new WaitForSeconds(seconds);
        TogglePlayerInputs(true);
    }
}
