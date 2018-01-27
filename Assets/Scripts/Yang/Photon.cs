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
    public float curEnergy = 0;

    public float minSize = 0.2f;
    public float maxSize = 1f;

    public float minVelocity = 2f;
    public float maxVelocity = 10f;

    public float killWaitTime = 2f;

    public AnimationCurve sizeCurve;
    public AnimationCurve velocityCurve;

    public MediumProp mediumProp;

    [Range(0f, 1f)]
    public float trailWidthFactor = 0.95f;

    bool _canScatter = true;
    public bool canScatter {
        get {
            return _canScatter;
        }
    }
    Coroutine scatterImmuneRoutine = null;
    Coroutine killSelfRoutine = null;

    [HideInInspector]
    public Collider2D lastScatterColl = null;

    Rigidbody2D rb;
    TrailRenderer trail;

    Vector3 initScale;

    Transform renderTransform;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();

        if(curEnergy == 0f)
        {
            curEnergy = maxEnergy;
        }

        renderTransform = GetComponentInChildren<Renderer>().transform;

        initScale = renderTransform.lossyScale;
        Debug.Log(initScale);

        renderTransform.localScale = new Vector3
            (initScale.x * maxSize, initScale.y * maxSize, initScale.z * maxSize);

        // rb.velocity = transform.up * maxVelocity;
        rb.AddForce(transform.up * maxVelocity, ForceMode2D.Impulse);

        trail = GetComponentInChildren<TrailRenderer>();
        trail.widthMultiplier = trailWidthFactor;
        // initTrailWidthMultiplier = trail.widthMultiplier;
        // Debug.Log(initTrailWidthMultiplier);

        
	}
	
	// Update is called once per frame
	void Update () {
        float curSize = GetCurrentSize();

        Vector3 parentScale = transform.localScale;

        renderTransform.localScale = new Vector3
            (initScale.x * curSize / parentScale.x, 
            initScale.y * curSize / parentScale.x, 
            initScale.z * curSize / parentScale.x);

        trail.widthMultiplier = initScale.x * curSize * trailWidthFactor;
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
        //rb.AddForce(-0.5f * rb.velocity.normalized * deltaSpeed, ForceMode2D.Impulse);

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

    public void InstantDead()
    {
        // Destroy(gameObject);
        ManageDeath();
    }

    void ManageDeath()
    {
        // Destroy(this.gameObject);
        rb.simulated = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<Renderer>().enabled = false;

        if(killSelfRoutine == null)
        {
            killSelfRoutine = StartCoroutine(KillSelf(killWaitTime));
        }
    }

    IEnumerator KillSelf(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }

    public void StartScatterImmune(float immuneTime)
    {
        if(scatterImmuneRoutine != null)
        {
            StopCoroutine(scatterImmuneRoutine);
            scatterImmuneRoutine = null;
        }
        
        scatterImmuneRoutine = StartCoroutine(ScatterImmuneIE(immuneTime));
        
    }

    IEnumerator ScatterImmuneIE(float immuneTime)
    {
        _canScatter = false;
        yield return new WaitForSeconds(immuneTime);
        _canScatter = true;
    }
}
