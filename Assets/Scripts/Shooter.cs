using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject projectisle;
    [SerializeField, Range(0f, 20f)] private float force = 4f;

    public void Shoot(Vector2 direction, Vector3 point)
    {
        GameObject obj = Instantiate(projectisle, point, Quaternion.Euler(0, 0, 0));
        obj.transform.SetParent(gameObject.transform, true);
        if (direction.x < 0)
        {
            obj.GetComponent<SpriteRenderer>().flipX = true;
        }
        obj.SetActive(true);
        obj.GetComponent<Rigidbody2D>().velocity = direction * force;
    }
}
