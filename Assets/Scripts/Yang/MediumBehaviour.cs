using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumBehaviour : MonoBehaviour {

    public MediumProp mediumProp;

    public string photonTag = "Photon";
    private Collider2D mediumColl;

	// Use this for initialization
	void Start () {
        mediumColl = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.tag == photonTag && TestColliderCenter(coll))
            // test where the center point is in collider
        {

            Photon photon = coll.GetComponent<Photon>();
            photon.mediumProp = mediumProp;
        }
    }

    private bool TestColliderCenter(Collider2D coll)
    {
        return mediumColl.OverlapPoint(coll.transform.position);
    }

}
