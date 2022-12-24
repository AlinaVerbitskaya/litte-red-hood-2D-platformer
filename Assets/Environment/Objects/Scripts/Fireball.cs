using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FixedJoint2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PointEffector2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Fireball : MonoBehaviour
{
    [SerializeField] private AudioClip explotionSound;
    [SerializeField, Range(0f, 50f)] private float shootingSpeed = 20;
    private bool exploded = false;

    public IEnumerator Shoot(Vector2 direction)
    {
        gameObject.GetComponent<FixedJoint2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * shootingSpeed);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.GetComponent<FixedJoint2D>().enabled == false && !exploded) {
            exploded = true;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<Animator>().SetTrigger("Explode");
            gameObject.GetComponent<PointEffector2D>().enabled = true;
            gameObject.GetComponent<AudioSource>().clip = explotionSound;
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject, 1f);
        }
    }
}
