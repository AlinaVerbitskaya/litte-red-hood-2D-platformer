using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Checkpoint : MonoBehaviour
{    
    public Transform teleportPoint;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Light lampLight;
    [SerializeField] private bool active = false;
    private float lightIntensity = 3.61f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !active)
        {
            EventManager.OnCheckpointActivated?.Invoke(this);
            StartCoroutine(Activate());
        }
    }

    public IEnumerator Activate()
    {
        active = true;
        particles.Play();
        for (float i = 0; i < 1f; i+= Time.deltaTime)
        {
            lampLight.intensity = lightIntensity * i;
            yield return null;
        }
    }

    public IEnumerator Deactivate()
    {
        active = false;
        particles.Stop();
        for (float i = 1; i > 0f; i -= Time.deltaTime)
        {
            lampLight.intensity = lightIntensity * i;
            yield return null;
        }
    }
}
