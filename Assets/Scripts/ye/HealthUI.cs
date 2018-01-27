using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    public PlayerStatus ps;

    Image healthImage;

    float dataStart;

    float dataEnd;

	// Use this for initialization
	void Start () {
        healthImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if(ps != null)
        {
            dataStart = healthImage.fillAmount;
            dataEnd = (float)ps.health / ps.maxHealth;
            healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, dataEnd, 0.8f);
        }
	}
}
