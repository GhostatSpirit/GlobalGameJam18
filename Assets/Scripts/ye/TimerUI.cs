using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour {

    Text text;

    public GameTimer gt;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(gt != null)
        {
            text.text = ((int)gt.realTime).ToString();
        }

	}
}
