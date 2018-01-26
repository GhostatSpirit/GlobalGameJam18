using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour {

    public PlayerStatus targetStatus; 

    BoxCollider2D playerCollider;

	// Use this for initialization
	void Start () {
        playerCollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(targetStatus != null)
        {
            targetStatus.addScore(1);
        }
    }

}
