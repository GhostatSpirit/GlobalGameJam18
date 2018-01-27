using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour {

    public PlayerStatus targetStatus;

    public Shooter shooter;

    public ParticleSystem ps;

    public string photonTag = "Photon";
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Hit!");

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
        Debug.Log("Hit!");

        if (collision.gameObject.tag == photonTag)
        {
            
            Photon photon = collision.transform.GetComponent<Photon>();
            photon.curEnergy = 0f;
            if (targetStatus != null && collision.gameObject.GetComponent<Photon>().shooter == shooter)
            {
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
                targetStatus.addScore(1);

            }
        }
    }


}
