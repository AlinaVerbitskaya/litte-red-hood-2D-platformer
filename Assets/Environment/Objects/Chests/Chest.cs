using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    [Header("Loot")]
    [SerializeField] private int itemTypeCount = 0;
    [SerializeField] private Collectible[] lootType;
    [SerializeField] private int[] lootCounts;
    [SerializeField] private Transform lootSpawnPoint;
    private bool activated = false;

    [Header("Hint")]
    [SerializeField] private Canvas hintCanvas;

    //Animation.
    private AudioSource openingSound;
    private Animator chestAnim;

    void Start()
    {
        chestAnim = GetComponent<Animator>();
        openingSound = GetComponent<AudioSource>();
        hintCanvas.gameObject.SetActive(true);
        ShowHint(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activated)
        {
            ShowHint(true);
            StartCoroutine(OpenOnKeyDown());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowHint(false);
        }
    }

    private IEnumerator OpenOnKeyDown()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E) || !hintCanvas.enabled);
        if (hintCanvas.enabled)
        {
            ShowHint(false);
            activated = true;
            chestAnim.SetTrigger("Open");
            openingSound.Play();
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(SpawnLoot());
        }
        
    }

    public void ShowHint(bool show)
    {
        hintCanvas.enabled = show;
    }

    private IEnumerator SpawnLoot()
    {
        Vector3 dir;
        for (int i = 0; i < itemTypeCount; i++)
        {
            for (int j = 0; j < lootCounts[i]; j++)
            {
                dir = new Vector3(Random.Range(-0.5f, 0.5f), 1);
                dir.Normalize();
                Instantiate(lootType[i], lootSpawnPoint.position, Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody2D>().velocity = dir * 4f;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
