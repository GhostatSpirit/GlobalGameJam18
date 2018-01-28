using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

    public float speed = 10f;
    [Range(0f, 1f)]
    public float lerpFactor = 0.5f;

    public PatrolPath patrolPath;

    public bool looping = true;
    bool forwarding = true;

    public bool snapFirstNode = true;

    List<Vector3> path;
    int targetIndex = 0;

	// Use this for initialization
	void Start () {
        path = patrolPath.path;
        if(path == null)
        {
            path = new List<Vector3>();
        }

        targetIndex = 0;

        if (snapFirstNode)
        {
            transform.position = path[0];
        }
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = path[targetIndex];

        Vector3 framePos = Vector3.Lerp(transform.position, targetPos, lerpFactor);
        Vector3 moveVector = framePos - transform.position;

        moveVector = Vector3.ClampMagnitude(moveVector, speed * Time.deltaTime);

        transform.position += moveVector;

        if(transform.position == targetPos)
        {
            targetIndex = GetNextIndex();
        }
	}


    int GetNextIndex()
    {
        if (looping)
        {
            int next = targetIndex + 1;
            if(next >= path.Count)
            {
                next = 0;
            }
            return next;
        } else
        {
            int next = targetIndex;
            if (forwarding) next++;
            else next--;

            if(next == path.Count - 1 && forwarding)
            {
                forwarding = false;
            } else if(next == 0 && !forwarding)
            {
                forwarding = true;
            }

            return next;
        }
    }
}
