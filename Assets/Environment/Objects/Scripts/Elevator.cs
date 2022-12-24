using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Elevator starting speed")]
    [SerializeField, Range(-3f, 3f)] float _speed = 1.0f;
    private SliderJoint2D _slJoint;

    void Start()
    {
        _slJoint = GetComponent<SliderJoint2D>();
        SpeedUpdate(_speed);
    }

    void LateUpdate()
    {
        MotorCheck();
    }

    /// <summary>
    /// Inverts elevator speed if it reached on a limit.
    /// </summary>
    private void MotorCheck()
    {
        if (_slJoint.limitState == JointLimitState2D.LowerLimit)
        {
            SpeedUpdate(_speed);
        }
          if(_slJoint.limitState == JointLimitState2D.UpperLimit)
        {
            SpeedUpdate(-_speed);
        }
    }

    private void SpeedUpdate(float speed)
    {
        JointMotor2D motor = _slJoint.motor;
        motor.motorSpeed = speed;
        _slJoint.motor = motor;
    }
}
