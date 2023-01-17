using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class wind : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private AreaEffector2D windArea;
    [SerializeField] private float force = 10f;
    [SerializeField] private float windTimer = 1f;
    [SerializeField] private float restTimer = 2f;
    private bool active = false;

    private void OnEnable()
    {
        StartCoroutine(WindCycle());
    }

    private void OnDisable()
    {
        StopCoroutine(WindCycle());
    }

    public IEnumerator WindCycle()
    {
        while (true)
        {
            yield return StartCoroutine(Activate());
            yield return new WaitForSeconds(windTimer);
            yield return StartCoroutine(Deactivate());
            yield return new WaitForSeconds(restTimer);
        }
    }

    public IEnumerator Activate()
    {
        active = true;
        particles.Play();
        for (float i = 0; i < 1f; i += Time.deltaTime)
        {
            windArea.forceMagnitude = force * i;
            yield return null;
        }
    }

    public IEnumerator Deactivate()
    {
        active = false;
        particles.Stop();
        for (float i = 1; i > 0f; i -= Time.deltaTime)
        {
            windArea.forceMagnitude = force * i;
            yield return null;
        }
    }
}
