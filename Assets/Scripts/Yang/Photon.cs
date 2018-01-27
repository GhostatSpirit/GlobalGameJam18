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

public class Photon : MonoBehaviour {
    public enum Shooter
    {
        Left, Right
    };

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

	}
	
	// Update is called once per frame
	void Update () {
        float curSize = GetCurrentSize();
        transform.localScale = new Vector3(curSize, curSize, curSize);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * GetCurrentVelocity();

        curEnergy -= mediumProp.energyFallRate * Time.fixedDeltaTime;

        if(curEnergy <= 0f)
        {
            ManageDeath();
        }
    }

    float GetCurrentVelocity()
    {
        // float normalizedEnergy = curEnergy / maxEnergy;
        // float eVeloFactor = velocityCurve.Evaluate(normalizedEnergy);
        return Mathf.Lerp(minVelocity, maxVelocity, mediumProp.velocityFactor);
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
