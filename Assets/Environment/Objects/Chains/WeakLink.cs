using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class WeakLink : MonoBehaviour
{
    void Death()
    {
        GetComponent<HingeJoint2D>().breakForce = 0;
    }
}
