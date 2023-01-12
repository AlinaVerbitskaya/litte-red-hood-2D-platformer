using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private enum CollectibleType {potion = 1, coin = 2 };
    [SerializeField] private CollectibleType type = CollectibleType.coin;
    [SerializeField] private AudioClip collectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case CollectibleType.coin:
                    EventManager.OnCoinCollected?.Invoke();
                    break;
                case CollectibleType.potion:
                    break;
                default:
                    break;
            }
            AudioSource.PlayClipAtPoint(collectSound, collision.transform.position + Vector3.up, 0.8f);
            Destroy(gameObject, 0.005f);
        }
    }


}
