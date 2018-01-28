using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setBoolFalse()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetBool("Hit",false);
    }
}
