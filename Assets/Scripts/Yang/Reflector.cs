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

    }

    void FixedUpdate()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // Debug.Log("Hit!");

        if(coll.gameObject.tag == photonTag)
        {
            if(coll.otherCollider.transform == transform)
            {

                if (coll.transform.tag != "Player")
                {
                    myAudioSource.PlayOneShot(reflectSound);
                }
            } else
            {
                // hit the back side
                Photon photon = coll.transform.GetComponent<Photon>();
                photon.curEnergy = 0f;
            }

        }

    }


}
