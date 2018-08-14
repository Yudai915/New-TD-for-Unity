using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeEff : MonoBehaviour {

	void Start () {
        StartCoroutine("Destroy");
	}
	
	void Update () {
		
	}

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
