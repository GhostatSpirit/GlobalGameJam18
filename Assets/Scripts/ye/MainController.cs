using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    public GameObject restartButton, endButton, winnerLeft, winnerRight, backgroundImg;

    public PlayerStatus playerAStatus, playerBStatus;

    public GameTimer gt;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerAStatus.health == 0 || playerBStatus.health == 0)
        {
            Time.timeScale = 0;
            restartButton.SetActive(true);
            endButton.SetActive(true);
            backgroundImg.SetActive(true);

            if(playerAStatus.health == 0)
            {
                winnerRight.SetActive(true);
            }
            else if (playerBStatus.health == 0)
            {
                winnerLeft.SetActive(true);
            }

        }
        else if(gt.realTime == 0 ){

            Time.timeScale = 0;
            restartButton.SetActive(true);
            endButton.SetActive(true);
            backgroundImg.SetActive(true);

            if (playerAStatus.health > playerBStatus.health)
            {
                winnerLeft.SetActive(true);
            }
            else if (playerAStatus.health < playerBStatus.health)
            {
                winnerRight.SetActive(true);
            }
        }
	}
}
