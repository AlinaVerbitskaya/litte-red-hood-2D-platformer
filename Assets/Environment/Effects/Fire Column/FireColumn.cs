using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class FireColumn : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float fireTimer = 1f;
    [SerializeField] private float restTimer = 2f;

    [Header("Animator")]
    [SerializeField] private Animator fireAnim;

    private void OnEnable()
    {
        StartCoroutine(FireCycle());
    }

    private void OnDisable()
    {
        StopCoroutine(FireCycle());
    }

    public IEnumerator FireCycle()
    {
        while (true)
        {
            fireAnim.SetBool("On", true);
            yield return new WaitForSeconds(fireTimer);
            fireAnim.SetBool("On", false);
            yield return new WaitForSeconds(restTimer);
        }
    }
}
