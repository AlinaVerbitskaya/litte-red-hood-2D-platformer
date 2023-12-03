using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class WeakLink : MonoBehaviour
{
    [SerializeField] private BoxCollider2D invisibleWall;

    void Death()
    {
        GetComponent<HingeJoint2D>().breakForce = 0;
        invisibleWall.enabled = false;
    }
}
