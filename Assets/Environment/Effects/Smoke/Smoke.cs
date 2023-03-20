using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(DamageDealer))]
public class Smoke : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private DamageDealer dd;
    [SerializeField, Range(0, 20f)] private int damage;
    [SerializeField, Range(0.1f, 10f)] private float timer = 3f;
    private float currentTimer = 0f;
    private bool active = false;

    void Start()
    {
        Reset(false);
    }

    void Update()
    {
        Tick();
    }

    private void Tick()
    {
        if (active)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= timer)
            {
                currentTimer = 0;
                dd.damage = damage;
            }
        }
    }

    private void Reset(bool act)
    {
        currentTimer = 0f;
        dd.damage = 0;
        active = act;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dd.IsInLayerMask(collision.gameObject, dd.damageLayerMask))
        {
            Reset(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (dd.IsInLayerMask(collision.gameObject, dd.damageLayerMask))
        {
            Reset(false);
        }
    }
}
