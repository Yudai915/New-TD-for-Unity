using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laserrange : MonoBehaviour {



	void Start () {
		
	}
	
	void Update () {
        if (Input.GetKeyUp(KeyCode.I) || Input.GetButtonUp("ButtonA"))
        {
            Destroy(gameObject);
        }
	}
}
