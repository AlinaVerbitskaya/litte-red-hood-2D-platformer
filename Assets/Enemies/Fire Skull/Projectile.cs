using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : DamageDealer
{
    [Header("Velocity")]
    public Vector3 direction = new Vector3(-1, 0, 0);
    [SerializeField, Range(0.01f, 1f)] private float speed = 0.05f;

    [Header("Lifespan")]
    [SerializeField, Range(1f, 10f)] private float lifespan = 3f;
    private bool activated = false;

    private void OnEnable()
    {
        activated = true;
        Destroy(gameObject, lifespan);
    }

    private void FixedUpdate()
    {
        if (activated) Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsInLayerMask(collision.gameObject, damageLayerMask))
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        gameObject.transform.position += direction * speed;
    }
}
