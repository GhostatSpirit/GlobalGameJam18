using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public KeyCode upButton;

    public KeyCode downButton;

    public float force;

    public GameObject bullet;

    public Transform firePosition;

    public float shootingTimeLoop = 5;

    public float bulletForce = 40;

    Rigidbody2D playerRigidbody;

    Vector2 forceVector;

    GameObject newBullet;

    float timeSum = 0;

	// Use this for initialization
	void Start () {
        playerRigidbody = GetComponent<Rigidbody2D>();
        Debug.Log(gameObject + " " + firePosition.rotation);
        ShootingController();
    }
	
	// Update is called once per frame
	void Update () {
        MovementController();

        if(timeSum > shootingTimeLoop)
        {
            ShootingController();
            timeSum = 0;
        }
        else
        {
            timeSum += Time.deltaTime;
        }

	}

    void MovementController()
    {
        if (Input.GetKey(upButton))
        {
            forceVector = new Vector2(0, force * Time.fixedDeltaTime);
            playerRigidbody.AddForce(forceVector);
        }

        else if (Input.GetKey(downButton))
        {
            forceVector = new Vector2(0, -force * Time.fixedDeltaTime);
            playerRigidbody.AddForce(forceVector);
        }
    }

    void ShootingController()
    {
        newBullet = Instantiate(bullet, firePosition.position, firePosition.rotation);
        newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(newBullet.transform.right.x, newBullet.transform.right.y) * bulletForce);
    }

}
