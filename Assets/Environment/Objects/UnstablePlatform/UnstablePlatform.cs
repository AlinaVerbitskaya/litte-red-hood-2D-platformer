using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(FrictionJoint2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class UnstablePlatform : MonoBehaviour
{
    [SerializeField] private float timeBeforeBreaking = 1f;
    [SerializeField] private float coolDown;
    private Collider2D[] colliders;
    private Rigidbody2D RB;
    private FrictionJoint2D FJ;
    private SpriteRenderer SR;
    private bool broken = false;

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        FJ = GetComponent<FrictionJoint2D>();
        SR = GetComponent<SpriteRenderer>();
        colliders = GetComponents<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool breaking = collision.gameObject.CompareTag("Player") && !broken && (collision.transform.position.y > transform.position.y);
        if (breaking)
        {
            StartCoroutine(Break());
        }
    }

    private IEnumerator Break()
    {
        // platform breaks
        broken = true;
        yield return new WaitForSeconds(timeBeforeBreaking);
        FJ.enabled = false;
        for (float i = 1f; i >= 0f; i -= Time.deltaTime)
        {
            SR.color = new Color(1, 1, 1, i);
            yield return null;
        }
        foreach (Collider2D c in colliders)
        {
            c.enabled = false;
        }
        RB.bodyType = RigidbodyType2D.Kinematic;
        yield return new WaitForSeconds(coolDown);

        // platform returns
        RB.velocity = Vector3.zero;
        transform.localPosition = Vector3.zero;
        FJ.enabled = true;
        for (float i = 0f; i <= 1f; i += Time.deltaTime)
        {
            SR.color = new Color(1, 1, 1, i);
            yield return null;
        }
        foreach (Collider2D c in colliders)
        {
            c.enabled = true;
        }
        RB.bodyType = RigidbodyType2D.Dynamic;
        broken = false;
    }
}
