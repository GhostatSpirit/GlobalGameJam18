using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MediumProp
{
    public float energyFallRate = 1f;
    [Range(0f, 1f)]
    public float velocityFactor = 1f;
}

public enum Shooter
{
    Left, Right
};

public class Photon : MonoBehaviour {
    

    public Shooter shooter;

    public float maxEnergy = 10f;

    [SerializeField]
    public float curEnergy;

    public float minSize = 0.2f;
    public float maxSize = 1f;

    public float minVelocity = 2f;
    public float maxVelocity = 10f;


    public AnimationCurve sizeCurve;
    public AnimationCurve velocityCurve;

    public MediumProp mediumProp;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();

        curEnergy = maxEnergy;
        transform.localScale = new Vector3(maxSize, maxSize, maxSize);

        // rb.velocity = transform.up * maxVelocity;
        rb.AddForce(transform.up * maxVelocity, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
        float curSize = GetCurrentSize();
        transform.localScale = new Vector3(curSize, curSize, curSize);
    }

    private void FixedUpdate()
    {
       //  rb.velocity = rb.velocity.normalized * GetCurrentVelocity();

        curEnergy -= mediumProp.energyFallRate * Time.fixedDeltaTime;

        

        if (curEnergy <= 0f)
        {
            ManageDeath();
        }


        //float deltaSpeed = mediumProp.energyFallRate * Time.fixedDeltaTime;

        //rb.AddForce(- 0.5f * rb.velocity.normalized * deltaSpeed, ForceMode2D.Impulse);

    }

    public void SetVelocityFactor(float factor)
    {
        rb.velocity = rb.velocity.normalized * GetVelocityMag(factor);
    }

    float GetVelocityMag(float factor)
    {
        // float normalizedEnergy = curEnergy / maxEnergy;
        // float eVeloFactor = velocityCurve.Evaluate(normalizedEnergy);
        return Mathf.Lerp(minVelocity, maxVelocity, factor);
    }

    float GetCurrentSize()
    {
        float normalizedEnergy = curEnergy / maxEnergy;
        float sizeFactor = sizeCurve.Evaluate(normalizedEnergy);
        return Mathf.Lerp(minSize, maxSize, sizeFactor);
    }

    void ManageDeath()
    {
        Destroy(this.gameObject);
    }
}
