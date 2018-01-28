using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour {

    public PlayerStatus targetStatus;

    public PlayerStatus selfStatus;

    public Shooter shooter;

    public ParticleSystem ps;

    public ParticleSystem firePs;

    public Transform psTransform;

    float points;

    bool triggered = false;

    void Start()
    {
        triggered = false;    
    }

    void Update()
    {
        if (selfStatus.health < 0.5 * selfStatus.maxHealth && triggered == false)
        {
            Instantiate(firePs, psTransform);
            triggered = true;
        }
    }

    public string photonTag = "Photon";
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Hit!");

        if (coll.gameObject.tag == photonTag)
        {
            Photon photon = coll.transform.GetComponent<Photon>();
            photon.InstantDead();
            if (targetStatus != null)
            {
                targetStatus.addScore(1);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.tag == photonTag)
        {
            
            Photon photon = collision.transform.GetComponent<Photon>();

            points = photon.curEnergy;
            
                photon.InstantDead();
            if (targetStatus != null && collision.gameObject.GetComponent<Photon>().shooter == shooter)
            {
                //Debug.Log("Hit!");
                //Debug.Log("in");

                //Debug.Log(photon.transform.position);

                //Debug.Log(photon.GetComponent<Rigidbody2D>().velocity);

                RaycastHit2D hit = Physics2D.Raycast(photon.transform.position, photon.GetComponent<Rigidbody2D>().velocity);
                ParticleSystem newPs;
                if (hit == true)
                {
                    newPs = Instantiate(ps, (Vector3)hit.point, Quaternion.Euler(0, 0, 0));
                    Destroy(newPs, 1);
                    //Debug.Log("here??");
                }
                else
                {
                    hit = Physics2D.Raycast(photon.transform.position, -photon.GetComponent<Rigidbody2D>().velocity);
                    if(hit == true)
                    {
                        newPs = Instantiate(ps, (Vector3)hit.point, Quaternion.Euler(0, 0, 0));
                        Destroy(newPs, 1);
                        //Debug.Log("here?????");
                    }
                    else
                    {
                        //Debug.Log("wtf");
                    }
                }
                //Debug.Log("damn");
                selfStatus.MinusHealth(points);          
                //targetStatus.addScore(1);

            }
        }
    }


}
