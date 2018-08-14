using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    Vector3 pos;

	void Start () {
        StartCoroutine("Destroy");
	}
	
	void Update () {
        pos = transform.position;
        transform.position = new Vector3(pos.x - 30f * Time.deltaTime, pos.y, pos.z);
	}

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
