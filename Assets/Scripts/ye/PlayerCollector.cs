using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour {

    public PlayerStatus targetStatus;

    public PlayerStatus selfStatus;

    public Shooter shooter;

    public ParticleSystem ps;

    float points;

    public string photonTag = "Photon";
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Hit collision!");

        if (coll.gameObject.tag == photonTag)
        {
            Photon photon = coll.transform.GetComponent<Photon>();
            photon.curEnergy = 0f;
            if (targetStatus != null)
            {
                targetStatus.addScore(1);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit trigger!");

        if (collision.gameObject.tag == photonTag)
        {

            Debug.Log("Now trigger!");

            Photon photon = collision.transform.GetComponent<Photon>();

            points = photon.curEnergy;

            Debug.Log("photon: "+points);

            photon.curEnergy = 0f;
            if (targetStatus != null && collision.gameObject.GetComponent<Photon>().shooter == shooter)
            {
                RaycastHit2D hit = Physics2D.Raycast(photon.transform.position, photon.GetComponent<Rigidbody2D>().velocity);
                ParticleSystem newPs;
                if (hit == true)
                {
                    newPs = Instantiate(ps, (Vector3)hit.point, Quaternion.Euler(0, 0, 0));
                    Destroy(newPs, 1);
                }
                else
                {
                    hit = Physics2D.Raycast(photon.transform.position, -photon.GetComponent<Rigidbody2D>().velocity);
                    if(hit == true)
                    {
                        newPs = Instantiate(ps, (Vector3)hit.point, Quaternion.Euler(0, 0, 0));
                        Destroy(newPs, 1);
                    }
                    else
                    {
                    }
                }

                //Debug.Log("before minus: " + targetStatus.health+ " " + points);
                selfStatus.MinusHealth(points);
                //Debug.Log("after minus: " + targetStatus.health + " " + points);
                //targetStatus.addScore(1);
            }
        }
    }


}
