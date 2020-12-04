using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
    private bool pause = false;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            pause = !pause;
        }
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;



    }
}
