﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonDestroyer : MonoBehaviour {
    public string photonTag = "Photon";
    void OnCollisionEnter2D(Collision2D coll)
    {
        TryDestory(coll);
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        TryDestory(coll);
    }

    void TryDestory(Collision2D coll)
    {
        if (coll.gameObject.tag == photonTag)
        {
            Photon photon = coll.transform.GetComponent<Photon>();
            photon.InstantDead();
        }
    }
}
