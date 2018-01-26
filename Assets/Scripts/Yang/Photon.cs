using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MediumProp
{
    public float energyFallRate = 1f;
    public float velocityFactor = 0f;
}

public class Photon : MonoBehaviour {

    public float maxEnergy = 10f;
    public float energyFallRate = 0f;

    [SerializeField]
    public float curEnergy;

    public float minSize = 0.2f;
    public float maxSize = 1f;

    public float minVelocity = 2f;
    public float maxVelocity = 10f;

    [Range(0f, 1f)]
    public float mediumVelocityFactor = 1f;

    public AnimationCurve sizeCurve;
    public AnimationCurve velocityCurve;


    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();

        curEnergy = maxEnergy;
        transform.localScale = new Vector3(maxSize, maxSize, maxSize);

	}
	
	// Update is called once per frame
	void Update () {
        float curSize = GetCurrentSize();
        transform.localScale = new Vector3(curSize, curSize, curSize);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * GetCurrentVelocity();

        curEnergy -= energyFallRate * Time.fixedDeltaTime;

    }

    float GetCurrentVelocity()
    {
        float normalizedEnergy = curEnergy / maxEnergy;

        // float eVeloFactor = velocityCurve.Evaluate(normalizedEnergy);
        return Mathf.Lerp(minVelocity, maxVelocity, mediumVelocityFactor);
    }

    float GetCurrentSize()
    {
        float normalizedEnergy = curEnergy / maxEnergy;
        float sizeFactor = sizeCurve.Evaluate(normalizedEnergy);
        return Mathf.Lerp(minSize, maxSize, sizeFactor);
    }
}
