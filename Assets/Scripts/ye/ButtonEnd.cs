using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndButtonOnClicked()
    {
        Application.Quit();
    }

    public void GameEndButtonOnClicked(int index)
    {
        SceneManager.LoadScene(index);
    }
}
