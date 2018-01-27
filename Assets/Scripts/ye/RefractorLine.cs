using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractorLine : MonoBehaviour {

    public float refractionK;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("in");

        Rigidbody2D hitRigidbody = collision.GetComponent<Rigidbody2D>();

        RaycastHit2D hit = Physics2D.Raycast(collision.transform.position, hitRigidbody.velocity);

        Vector2 velocityNormalized = hitRigidbody.velocity.normalized;

        Vector2 normalDirection = transform.right;

        float angle = GetAngle(velocityNormalized, normalDirection);

  //      Debug.Log(angle);

        Vector3 lastVelocity = hitRigidbody.velocity;
        Vector3 refractedVelocity = lastVelocity;

        //Debug.Log(angle);
        if ((angle <= 1 && angle >= -1)|| (angle >= 179 && angle <= 180)|| (angle <= -179 && angle >= -180))
        {
            float t = Random.value;

            t = (t > 0.5) ? 1 : -1;

            Quaternion refractRot = Quaternion.Euler(0f, 0f, 5f * t);
            refractedVelocity = refractRot * lastVelocity;


            // collision.transform.Rotate(0, 0, 10 * t);
        }
        else
        {
            Debug.Log("here");
            if(angle > 90)
            {
                angle = 180 - angle;
            }
            else if(angle < -90)
            {
                angle = -angle - 180;
            }

            Quaternion refractRot = Quaternion.Euler(0f, 0f, refractionK * angle);
            refractedVelocity = refractRot * lastVelocity;

            Debug.Log(angle);
            // collision.transform.Rotate(0, 0, refractionK * angle);
        }

        hitRigidbody.velocity = refractedVelocity;

    }

    private static float GetAngle(Vector2 v1, Vector2 v2)
    {
        var sign = Mathf.Sign(v1.x * v2.y - v1.y * v2.x);
        return Vector2.Angle(v1, v2) * sign;
    }
}
