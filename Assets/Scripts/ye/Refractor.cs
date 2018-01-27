using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refractor : MonoBehaviour {
    public string photonTag = "Photon";
    public AudioClip reflectSound;
    AudioSource myAudioSource;

    public float refractionK = 0.5f;
    // Use this for initialization
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D hitRigidbody = collision.GetComponent<Rigidbody2D>();

        RaycastHit2D hit = Physics2D.Raycast(collision.transform.position, - hitRigidbody.velocity);

        Vector2 velocityNormalized = hitRigidbody.velocity.normalized;

        Vector2 normalDirection = ((Vector2)transform.position - hit.point).normalized;

        float angle = GetAngle(velocityNormalized, normalDirection);

        if(angle <= 10)
        {
            float t = Random.value;

            t = (t > 0.5) ? 1 : -1;

            collision.transform.Rotate(0, 0, 10 * t);
        }
        else
        {
            collision.transform.Rotate(0, 0, refractionK * angle);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Rigidbody2D hitRigidbody = collision.GetComponent<Rigidbody2D>();

        RaycastHit2D hit = Physics2D.Raycast(collision.transform.position, -hitRigidbody.velocity);

        Vector2 velocityNormalized = hitRigidbody.velocity.normalized;

        Vector2 normalDirection = ((Vector2)transform.position - hit.point).normalized;

        float angle = GetAngle(velocityNormalized, -normalDirection);

        float finalAngle = refractionK * angle;

        finalAngle = finalAngle - (int)finalAngle / 180 * 180;

        collision.transform.Rotate(0, 0, refractionK * angle);
    }

    private static float GetAngle(Vector2 v1, Vector2 v2)
    {
        var sign = Mathf.Sign(v1.x * v2.y - v1.y * v2.x);
        return Vector2.Angle(v1, v2) * sign;
    }


}
