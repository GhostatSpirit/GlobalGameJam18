using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour {

    public PlayerStatus targetStatus; 

    public string photonTag = "Photon";
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Hit!");

        if (coll.gameObject.tag == photonTag)
        {
            Photon photon = coll.transform.GetComponent<Photon>();
            photon.curEnergy = 0f;
            if (targetStatus != null)
            {
                targetStatus.addScore(1);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit!");

        if (collision.gameObject.tag == photonTag)
        {
            Photon photon = collision.transform.GetComponent<Photon>();
            photon.curEnergy = 0f;
            if (targetStatus != null)
            {
                targetStatus.addScore(1);
            }
        }
    }


}
