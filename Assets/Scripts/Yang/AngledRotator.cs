using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngledRotator : MonoBehaviour {

    public float speed = 45f;

    [Range(0f, 1f)]
    public float lerpFactor = 0.5f;

    [Range(0f, 360f)]
    public float angleRange = 60f;

    public float freezeTime = 1f;

    public bool forwarding = true;

    float halfRange;
    Quaternion initRot;
    Vector3 initUp;

    float targetAngle = 0f;
    bool canMove = true;


    // Use this for initialization
    void Start () {
        halfRange = angleRange / 2f;

        initRot = transform.rotation;
        initUp = transform.up;

        StartCoroutine(AngledRotateIE());
	}
	
	// Update is called once per frame
	void Update () {

        if (!canMove)
        {
            return;
        }

        Quaternion frameUpRot = Quaternion.FromToRotation(initUp, transform.up);
        float curAngle = frameUpRot.eulerAngles.z;
        if(curAngle > 180f)
        {
            curAngle -= 360f;
        }
        //Debug.Log(curAngle);


       //  targetAngle = 0f;

        if (forwarding)
        {
            targetAngle = Mathf.MoveTowards(curAngle, halfRange, speed * Time.deltaTime);
            //if (Mathf.Approximately(targetAngle, halfRange))
            //{
            //    forwarding = false;
            //}
        } else
        {
            targetAngle = Mathf.MoveTowards(curAngle, -halfRange, speed * Time.deltaTime);
            //if (Mathf.Approximately(targetAngle, -halfRange))
            //{
            //    forwarding = true;
            //}

        }

        Quaternion frameRot = Quaternion.Euler(0f, 0f, targetAngle);

        transform.rotation = initRot * frameRot;
	}


    IEnumerator AngledRotateIE()
    {
        while (true)
        {
            if (forwarding)
            {
                yield return new WaitUntil(() => Mathf.Approximately(targetAngle, halfRange));
                forwarding = false;

                canMove = false;
                yield return new WaitForSeconds(freezeTime);
                canMove = true;

            } else
            {
                yield return new WaitUntil(() => Mathf.Approximately(targetAngle, -halfRange));
                forwarding = true;

                canMove = false;
                yield return new WaitForSeconds(freezeTime);
                canMove = true;
            }
            
        }
    }

}
