using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public KeyCode upButton;

    public KeyCode downButton;

    public float force;

    public GameObject bullet;

    public Transform firePosition;

    public GameTimer gt;

    public AnimationCurve shootingTimeCurve;

    [HideInInspector]public float shootingTimeLoop = 1.0f;

    public float bulletForce = 40;

    public AudioClip shootClip;

    Rigidbody2D playerRigidbody;

    Vector2 forceVector;

    GameObject newBullet;

    AudioSource audioSource;

    float timeSum = 0;

	// Use this for initialization
	void Start () {
        playerRigidbody = GetComponent<Rigidbody2D>();
        ShootingController();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        shootingTimeLoop = shootingTimeCurve.Evaluate((float)gt.realTime / gt.limitTime);

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
            playerRigidbody.AddForce(forceVector, ForceMode2D.Impulse);
        }

        else if (Input.GetKey(downButton))
        {
            forceVector = new Vector2(0, -force * Time.fixedDeltaTime);
            playerRigidbody.AddForce(forceVector, ForceMode2D.Impulse);
        }
    }

    void ShootingController()
    {
        Instantiate(bullet, firePosition.position, firePosition.rotation * Quaternion.Euler(0, 0, -90));

        if(audioSource && shootClip)
        {
            audioSource.PlayOneShot(shootClip);
        }
    }

}
