using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerTag {none, left, right};

public class BlackHole : MonoBehaviour {

    public string photonTag = "Photon";

    public PlayerTag controlTag = PlayerTag.none;

    public Color leftColor, rightColor;

    public float triggerNum = 100;

    public float spawnNum = 8;

    public float countNum = 0;

    //public bool random = false;

    //public float refractionK = 0.5f;

    public GameObject photonLeft;

    public GameObject photonRight;

    GameObject bullet;

    SpriteRenderer sr;

    Shooter shooter;

    public float privateRotationSpeed = 20;

    public float publicRotationSpeed = 20;

    public GameObject rotationTarget;

    public float respawnTime = 2.0f;

    // Use this for initialization
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        gameObject.transform.Rotate(0, 0, privateRotationSpeed * Time.deltaTime);

        gameObject.transform.RotateAround(rotationTarget.transform.position, Vector3.forward, publicRotationSpeed * Time.deltaTime);

        if (controlTag == PlayerTag.left)
        {
            bullet = photonLeft;
        }
        else if (controlTag == PlayerTag.right)
        {
            bullet = photonRight;
        }

        switch (controlTag)
        {
            //case PlayerTag.none:
            //    sr.color = Color.white;
            //    break;
            case PlayerTag.left:
                sr.color = Color.Lerp(Color.white, leftColor, countNum / triggerNum);
                break;
            case PlayerTag.right:
                sr.color = Color.Lerp(Color.white, rightColor, countNum / triggerNum);
                break;
            default:
                break;
        }
    }

    void ShootingController()
    {
        //gameObject.SetActive(false);
        StartCoroutine(WaitToRespawn());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        shooter = collision.GetComponent<Photon>().shooter;
        if (collision.tag == photonTag)
        {
            if (controlTag == PlayerTag.none)
            {
                switch (shooter)
                {
                    case Shooter.Left:
                        controlTag = PlayerTag.left;
                        break;
                    case Shooter.Right:
                        controlTag = PlayerTag.right;
                        break;
                }
                
            }

            if ((controlTag == PlayerTag.left && shooter == Shooter.Left)
                || (controlTag == PlayerTag.right && shooter == Shooter.Right))
            {
                countNum += 1;
                Debug.Log("getHere!");
            }
            else if ((controlTag == PlayerTag.right && shooter == Shooter.Left)
                || (controlTag == PlayerTag.left && shooter == Shooter.Right))
            {
                countNum -= 1;
            }

            if (countNum < 0)
            {
                countNum = 0;
            }

            collision.GetComponent<Photon>().InstantDead();

            if (countNum == 0)
            {
                //Debug.Log("getHere!");
                controlTag = PlayerTag.none;
                sr.color = Color.white;
            }

            if (countNum == triggerNum)
            {
                ShootingController();
            }
        }
    }

    private static float GetAngle(Vector2 v1, Vector2 v2)
    {
        var sign = Mathf.Sign(v1.x * v2.y - v1.y * v2.x);
        return Vector2.Angle(v1, v2) * sign;
    }

    IEnumerator WaitToRespawn()
    {
        //transform.localScale  = new Vector3(Mathf.Lerp(transform.localScale.x, 0, 2.0f), Mathf.Lerp(transform.localScale.y, 0, 2.0f), 1.0f);

        //yield return new WaitForSeconds(respawnTime);
        for (int i = 0; i < spawnNum; i++)
        {
            Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation * Quaternion.Euler(0, 0, i * 360 / spawnNum));
        }

        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        sr.color = Color.black;
        controlTag = PlayerTag.none;
        countNum = 0;

        yield return new WaitForSeconds(respawnTime);
        sr.color = Color.white;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
}

