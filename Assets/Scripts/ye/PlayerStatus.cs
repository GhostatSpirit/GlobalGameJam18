using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    [HideInInspector] public float score = 0;

    [HideInInspector] public float health = 0;

    public float maxHealth = 100;

	// Use this for initialization
	void Start () {
        health = maxHealth;	
	}
	
	// Update is called once per frame
	void Update () {
        if(health <= 0)
        {
            health = 0;
        }
        //Debug.Log(health);
	}

    public void addScore(float hitScore)
    {
        score += hitScore;
    }

    public void MinusHealth(float points)
    {
        //Debug.Log("minus now");
        if(health > 0)
        {
            health -= points;
        }

    }

}
