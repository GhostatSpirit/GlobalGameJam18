using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    public float limitTime;

    [HideInInspector]
    public float realTime = 0;

    Text text;

	// Use this for initialization
	void Start () {
        realTime = limitTime;
        text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        realTime -= Time.deltaTime;
        text.text = ((int)realTime).ToString();
	}
}
