using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scatterer : MonoBehaviour {

    public float minScatterEnergy = 9f;
    public float minScatterAngle = 30f;

    public int scatterCount = 3;

    [Range(0f, 180f)]
    public float scatterAngle = 90f;

    // after scattering, the new scattered photon will be prevented from scattering
    // for xx seconds
    public float scatterImmuneTime = 1f;

    public string photonTag = "Photon";

    private Collider2D scatterColl;

    private void Start()
    {
        scatterColl = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag != photonTag)
        {
            return;
        }

        Photon photon = coll.transform.GetComponent<Photon>();
        Rigidbody2D photonRb = coll.transform.GetComponent<Rigidbody2D>();
        Transform photonTransform = coll.transform;
        GameObject photonGO = coll.gameObject;

        if (!photon.canScatter || photon.curEnergy < minScatterEnergy || photon.lastScatterColl)
        {
            return;
        }

        Vector3 veloDir = photonRb.velocity.normalized;

        float veloIncomingAngle = Vector3.Angle(veloDir, transform.right);
        if (veloIncomingAngle > 90f)  veloIncomingAngle = 180f - veloIncomingAngle;

        //Debug.Log(veloIncomingAngle);
        if(veloIncomingAngle < minScatterAngle)
        {
            return;
        }

        Vector3 normal;
        if (Vector3.Dot(veloDir, transform.up) > 0)
        {
            normal = transform.up;
        }
        else
        {
            normal = -transform.up;
        }


        // this photon could be scattered
        float deltaAngle = scatterAngle / (scatterCount - 1);
        float startAngle = -scatterAngle / 2f;
        float scatteredEnergy = photon.curEnergy / scatterCount;




        for(int i = 0; i < scatterCount; ++i)
        {
            float currentAngle = startAngle + i * deltaAngle;

            Quaternion scatterRot = Quaternion.Euler(0f, 0f, currentAngle);
            Vector3 scatteredDir = scatterRot * normal;
            Quaternion newRot = Quaternion.FromToRotation(Vector3.up, scatteredDir);

            GameObject newPhotonGO = Instantiate(photonGO, photonTransform.position, newRot);
            Photon newPhoton = newPhotonGO.GetComponent<Photon>();
            newPhoton.StartScatterImmune(scatterImmuneTime);
            newPhoton.lastScatterColl = scatterColl;
            newPhoton.curEnergy = scatteredEnergy;
        }

        photon.lastScatterColl = scatterColl;
        Destroy(photonGO);

    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag != photonTag)
        {
            return;
        }

        Photon photon = coll.transform.GetComponent<Photon>();
        if(photon.lastScatterColl == scatterColl)
        {
            photon.lastScatterColl = null;
        }
    }
}
