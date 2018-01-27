using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour {
    public string photonTag = "Photon";
    public AudioClip reflectSound;
    AudioSource myAudioSource;
    // Use this for initialization
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0,0,1);
    }

    void FixedUpdate()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Hit!");

        if (coll.gameObject.tag == photonTag)
        {
            if (coll.otherCollider.transform == transform)
            {
                // hit the front side of the mirror
                ContactPoint2D contact = coll.contacts[0];
                //Debug.Log ("Hit!");
                Debug.DrawRay(contact.point, contact.normal, Color.white);

                // play the reflect sound
                if (coll.transform.tag != "Player")
                {
                    myAudioSource.PlayOneShot(reflectSound);
                }

                Vector2 hitNormal = -contact.normal;

                Vector2 newDirection = Vector2.Reflect(coll.transform.up, hitNormal);
                coll.transform.up = newDirection.normalized;
                //coll.rigidbody.velocity = newDirection.normalized * 1;
            }
            else
            {
                // hit the back side
                Photon photon = coll.transform.GetComponent<Photon>();
                photon.curEnergy = 0f;
            }

        }

    }

}
