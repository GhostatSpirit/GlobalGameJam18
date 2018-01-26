using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonDestroyer : MonoBehaviour {
    public string photonTag = "Photon";
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Hit!");

        if (coll.gameObject.tag == photonTag)
        {
            Photon photon = coll.transform.GetComponent<Photon>();
            photon.curEnergy = 0f;
        }

    }
}
