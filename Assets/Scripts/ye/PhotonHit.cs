using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonHit : MonoBehaviour {

    public ParticleSystem ps;

    //public LayerMask layerMask;

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.layer);
        
        if(collision.gameObject.tag == "Photon")
        {
            if(collision.contacts.Length > 1)
            {
                Instantiate(ps, (Vector3)collision.contacts[0].point, Quaternion.Euler(0, 0, 0));
            }
            
        }
    }
}
