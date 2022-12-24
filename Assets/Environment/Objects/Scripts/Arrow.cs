using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : DamageDealer
{
    private bool expired = false;
    private float lifeSpan = 5f;

    private void Awake()
    {
        Destroy(gameObject, lifeSpan);
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (expired)
        {
            Death();
        }
        else if (IsInLayerMask(collision.gameObject, damageLayerMask))
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
            gameObject.transform.SetParent(collision.transform);
            expired = true;
        }
    }
}
