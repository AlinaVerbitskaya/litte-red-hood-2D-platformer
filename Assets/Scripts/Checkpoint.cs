using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool active = false;
    [SerializeField] private SpriteRenderer signSR;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !active)
        {
            StartCoroutine(Activate());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && active)
        {
            StartCoroutine(Deactivate());
        }
    }

    public IEnumerator Activate()
    {
        active = true;
        for (float i = 0; i < 1f; i+= Time.deltaTime)
        {
            signSR.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    public IEnumerator Deactivate()
    {
        active = false;
        for (float i = 1; i > 0f; i -= Time.deltaTime)
        {
            signSR.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
}
