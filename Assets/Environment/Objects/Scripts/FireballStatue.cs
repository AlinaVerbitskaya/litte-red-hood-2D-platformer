using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballStatue : MonoBehaviour
{
    [SerializeField] private Canvas hintCanvas;
    [SerializeField] private Fireball fireball;
    [SerializeField] private GameObject fireballHolder;
    [SerializeField] private Vector2 shootingDirection = Vector2.right;
    private bool activated = false;

    void Start()
    {
        hintCanvas.gameObject.SetActive(true);
        ShowHint(false);
    }

    /// <summary>
    /// Shoots a fireball copy in specified direction if activated.
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShootOnKeyDown()
    {        
        activated = true;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E) || !hintCanvas.enabled);
        if (!hintCanvas.enabled)
        {
            activated = false;
        }
        else
        {
            ShowHint(false);
            fireball.gameObject.SetActive(false);
            Fireball newFireball = Instantiate(fireball, fireballHolder.transform.position, Quaternion.Euler(0, 0, 0), fireballHolder.transform);
            newFireball.gameObject.SetActive(true);
            newFireball.StartCoroutine(newFireball.Shoot(shootingDirection));
            yield return new WaitForSeconds(1.5f);
            fireball.gameObject.SetActive(true);
            activated = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activated)
        {
            ShowHint(true);
            StartCoroutine("ShootOnKeyDown");
        }
    }
 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowHint(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activated)
        {
            ShowHint(true);
            StartCoroutine("ShootOnKeyDown");
        }
    }

    public void ShowHint(bool show)
    {
        hintCanvas.enabled = show;
    }
}
