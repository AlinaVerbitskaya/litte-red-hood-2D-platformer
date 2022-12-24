using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float defaultZ = -10f, offsetY = 2.5f;
    [SerializeField] private Transform target;
    [SerializeField] private Transform leftLimit, rightLimit, lowerLimit, upperLimit;

    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y + offsetY, defaultZ), 0.1f);
        if (newPos.x < leftLimit.position.x)
        {
            newPos.x = leftLimit.position.x;
        }
        if (newPos.x > rightLimit.position.x)
        {
            newPos.x = rightLimit.position.x;
        }
        if (newPos.y < lowerLimit.position.y)
        {
            newPos.y = lowerLimit.position.y;
        }
        if (newPos.y > upperLimit.position.y)
        {
            newPos.y = upperLimit.position.y;
        }
        transform.position = newPos;
    }
}
